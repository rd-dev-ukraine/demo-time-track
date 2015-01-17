using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using LanceTrack.Cqrs.Contract;
using LanceTrack.Domain.Invoicing;
using LanceTrack.Domain.Projects;
using LanceTrack.Domain.TimeTracking;
using LanceTrack.Server.Cqrs.ProjectTime.Commands;
using LanceTrack.Server.Cqrs.ProjectTime.Events;
using LanceTrack.Server.Cqrs.ProjectTime.State;

namespace LanceTrack.Server.Cqrs.ProjectTime
{
    public class ProjectTimeAggregateRoot : IAggregateRootWithState<ProjectTimeAggregateRootState, int>,
        ICommandHandler<TrackTimeCommand, ProjectTimeAggregateRoot, int>,
        ICommandHandler<BillProjectCommand, ProjectTimeAggregateRoot, int>,
        ICommandHandler<RecalculateInvoiceInfoCommand, ProjectTimeAggregateRoot, int>,
        IEventRecipient<TimeTrackedEvent, ProjectTimeAggregateRoot, int>,
        IEventRecipient<InvoiceEvent, ProjectTimeAggregateRoot, int>
    {
        private readonly IProjectService _projectService;

        public ProjectTimeAggregateRoot(
            IProjectService projectService,
            IEnumerable<IAggregateRootReadModelManager<ProjectTimeAggregateRoot, int>> readModelManagers)
        {
            if (projectService == null)
                throw new ArgumentNullException("projectService");
            if (readModelManagers == null)
                throw new ArgumentNullException("readModelManagers");

            _projectService = projectService;
            ReadModels = readModelManagers.ToArray();
            State = new ProjectTimeAggregateRootState();
        }

        public IEnumerable<IReadModelManager<int>> ReadModels { get; private set; }

        public ProjectTimeAggregateRootState State { get; private set; }

        public IEnumerable<IEvent<ProjectTimeAggregateRoot, int>> Execute(TrackTimeCommand command)
        {
            var project = _projectService.GetById(command.ProjectId);

            if (project == null || project.Status != ProjectStatus.Active)
                throw new ProjectNotReportableException();

            var targetUserProjectData = _projectService.GetProjectUserInfo(command.ForUserId, command.ProjectId);
            if (targetUserProjectData == null)
                throw new ProjectAuthorizationException("User is not assigned to the project.");

            var trackingPermissions = command.ForUserId == command.ByUserId ? ProjectPermissions.TrackSelf : ProjectPermissions.TrackAsOtherUser;
            var trackerUserData = _projectService.GetProjectUserInfo(command.ByUserId, project.Id);
            if (trackerUserData == null ||
                (trackerUserData.UserPermissions & trackingPermissions) == 0)
                throw new ProjectAuthorizationException();

            if (project.StartDate.Date > command.At.Date)
                throw new ProjectNotReportableException();
            if (project.EndDate != null && project.EndDate < command.At.Date)
                throw new ProjectNotReportableException();

            if (project.MaxTotalHoursPerDay != null &&
                command.Hours > project.MaxTotalHoursPerDay.Value)
                throw new IncorrectHoursException();

            var projectDailyHours = State.ProjectUserTime.Where(p => p.ProjectId == command.ProjectId);

            if (projectDailyHours.Any() &&
                (projectDailyHours.Sum(p => p.TotalHours) + command.Hours) > project.MaxTotalHours)
                throw new IncorrectHoursException();

            var @event = new TimeTrackedEvent
            {
                At = command.At,
                Hours = command.Hours,
                ProjectId = project.Id,
                RegisteredAt = DateTimeOffset.Now,
                RegisteredByUserId = command.ByUserId,
                UserId = command.ForUserId,
                HourlyRate = trackerUserData.HourlyRate
            };

            yield return @event;
        }

        public IEnumerable<IEvent<ProjectTimeAggregateRoot, int>> Execute(BillProjectCommand command)
        {
            CheckUserBillingRights(command.ProjectId, command.ByUserId);

            if (!command.InvoiceUserRequest.Any())
                throw new ArgumentException("Invoice is empty.");

            var invoiceNum = InvoiceNum(command.ProjectId);
            command.Result = invoiceNum;

            foreach (var userInvoiceInfo in command.InvoiceUserRequest)
            {
                if (_projectService.GetProjectUserInfo(userInvoiceInfo.UserId, command.ProjectId) == null)
                    throw new ArgumentException(String.Format("User {0} is not associated with project {1}.", userInvoiceInfo.UserId, command.ProjectId));

                var maxBillableHours = State.MaxBillableHours(userInvoiceInfo.UserId);
                if (maxBillableHours < userInvoiceInfo.Hours)
                    throw new ArgumentException(String.Format("Max {0} hours could be billed for user {1}.", maxBillableHours, userInvoiceInfo.UserId));

                var sum = State.CalculateInvoiceSum(userInvoiceInfo.UserId, userInvoiceInfo.Hours);

                var ev = new InvoiceEvent
                {
                    At = DateTimeOffset.Now,
                    EventType = InvoiceEventType.Billing,
                    Hours = userInvoiceInfo.Hours,
                    InvoiceSum = sum,
                    ProjectId = command.ProjectId,
                    UserId = userInvoiceInfo.UserId,
                    RegisteredAt = DateTimeOffset.Now,
                    RegisteredByUserId = command.ByUserId,
                    InvoiceNum = invoiceNum
                };

                yield return ev;
            }
        }

        public IEnumerable<IEvent<ProjectTimeAggregateRoot, int>> Execute(RecalculateInvoiceInfoCommand command)
        {
            CheckUserBillingRights(command.ProjectId, command.ByUserId);

            command.Result = new List<InvoiceRecalculationResult>();
            var projectUsers = _projectService.GetProjectUserInfo(command.ProjectId);

            foreach (var projectUserInfo in projectUsers)
            {
                var userInvoiceInfo = command.InvoiceUserRequest.SingleOrDefault(r => r.UserId == projectUserInfo.UserId) ??
                                        new InvoiceUserRequest
                                        {
                                            UserId = projectUserInfo.UserId,
                                            Hours = 0
                                        };
                
                var maxBillableHours = State.MaxBillableHours(userInvoiceInfo.UserId);

                if (maxBillableHours == 0)
                    continue; 

                var hours = Math.Min(userInvoiceInfo.Hours, maxBillableHours);

                var sum = State.CalculateInvoiceSum(userInvoiceInfo.UserId, hours);

                command.Result.Add(new InvoiceRecalculationResult
                {
                    BillingHours = hours,
                    MaxHours = maxBillableHours,
                    UserId = userInvoiceInfo.UserId,
                    Sum = sum
                });
            }

            yield break;
        }

        public void On(TimeTrackedEvent @event)
        {
            State.On(@event);
        }

        public void On(InvoiceEvent @event)
        {
            State.On(@event);
        }

        private void CheckUserBillingRights(int projectId, int userId)
        {
            var userData = _projectService.GetProjectUserInfo(userId, projectId);
            if (userData == null ||
                (userData.UserPermissions & ProjectPermissions.BillProject) == 0)
                throw new ArgumentException("User is not allowed to bill the project.");
        }

        private string InvoiceNum(int projectId)
        {
            return String.Format(CultureInfo.GetCultureInfo("en"), "INV{0:0000}/{1}-{2:yyyyMMMdd}", projectId, State.InvoiceCount() + 1, DateTime.Now);
        }
    }
}