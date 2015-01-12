using System;
using System.Collections.Generic;
using System.Linq;
using LanceTrack.Cqrs.Contract;
using LanceTrack.Domain.Invoicing;
using LanceTrack.Domain.TimeTracking;
using LanceTrack.Server.Cqrs.ProjectTime.Commands;
using LanceTrack.Server.Cqrs.ProjectTime.Events;
using LanceTrack.Server.Cqrs.ProjectTime.State;
using LanceTrack.Server.Dependencies.Project;

namespace LanceTrack.Server.Cqrs.ProjectTime
{
    public class ProjectTimeAggregateRoot : IAggregateRootWithState<ProjectTimeAggregateRootState, int>,
        ICommandHandler<TrackTimeCommand, ProjectTimeAggregateRoot, int>,
        ICommandHandler<BillProjectCommand, ProjectTimeAggregateRoot, int>,
        ICommandHandler<RecalculateInvoiceInfoCommand, ProjectTimeAggregateRoot, int>,
        IEventRecipient<ProjectTimeTrackedEvent, ProjectTimeAggregateRoot, int>,
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

            if (project.Status != ProjectStatus.Active)
                throw new ProjectNotReportableException();

            var projectUserData = _projectService.GetProjectUserData(command.UserId, project.Id);

            if (projectUserData == null || (projectUserData.UserPermissions & ProjectPermissions.TrackSelf) == 0)
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
                (projectDailyHours.Sum(p => p.Hours) + command.Hours) > project.MaxTotalHours)
                throw new IncorrectHoursException();

            var @event = new ProjectTimeTrackedEvent
            {
                At = command.At,
                Hours = command.Hours,
                ProjectId = project.Id,
                RegisteredAt = DateTimeOffset.Now,
                RegisteredByUserId = command.UserId,
                UserId = command.UserId,
                HourlyRate = projectUserData.HourlyRate
            };

            yield return @event;
        }

        public IEnumerable<IEvent<ProjectTimeAggregateRoot, int>> Execute(BillProjectCommand command)
        {
            CheckUserBillingRights(command.ProjectId, command.UserId);

            var maxBillableHours = State.MaxBillableHours(command.UserId);
            if (maxBillableHours < command.Hours)
                throw new ArgumentException(String.Format("Max {0} hours could be billed.", maxBillableHours));

            var sum = State.CalculateInvoiceSum(command.UserId, command.Hours);

            var invoiceNum = InvoiceNum(command.ProjectId);

            command.Result = invoiceNum;

            var ev = new InvoiceEvent
            {
                At = DateTimeOffset.Now,
                EventType = InvoiceEventType.Billing,
                Hours = command.Hours,
                InvoiceSum = sum,
                ProjectId = command.ProjectId,
                UserId = command.UserId,
                RegisteredAt = DateTimeOffset.Now,
                RegisteredByUserId = command.UserId,
                InvoiceNum = invoiceNum
            };

            yield return ev;
        }

        public IEnumerable<IEvent<ProjectTimeAggregateRoot, int>> Execute(RecalculateInvoiceInfoCommand command)
        {
            CheckUserBillingRights(command.ProjectId, command.UserId);
            var maxBillableHours = State.MaxBillableHours(command.UserId);
            var hours = Math.Min(command.BilledHours, maxBillableHours);

            var sum = State.CalculateInvoiceSum(command.UserId, hours);

            command.Result = new InvoiceRecalculationResult
            {
                BillingHours = hours,
                MaxHours = maxBillableHours,
                Sum = sum
            };

            yield break;
        }

        public void On(ProjectTimeTrackedEvent @event)
        {
            State.On(@event);
        }

        public void On(InvoiceEvent @event)
        {
            State.On(@event);
        }

        private void CheckUserBillingRights(int projectId, int userId)
        {
            var userData = _projectService.GetProjectUserData(userId, projectId);
            if (userData == null ||
                (userData.UserPermissions & ProjectPermissions.BillProject) == 0)
                throw new ArgumentException("User is not allowed to bill the project.");
        }

        private string InvoiceNum(int projectId)
        {
            return String.Format("INV{0:000000}-{1}/{2:yyyyMMdd}", projectId, State.Invoices.Count + 1, DateTimeOffset.Now);
        }
    }
}