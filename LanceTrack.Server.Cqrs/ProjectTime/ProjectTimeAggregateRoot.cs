using System;
using System.Collections.Generic;
using System.Linq;
using LanceTrack.Cqrs.Contract;
using LanceTrack.Domain.TimeTracking;
using LanceTrack.Server.Cqrs.ProjectTime.Commands;
using LanceTrack.Server.Cqrs.ProjectTime.Events;
using LanceTrack.Server.Cqrs.ProjectTime.State;
using LanceTrack.Server.Dependencies.Project;

namespace LanceTrack.Server.Cqrs.ProjectTime
{
    public class ProjectTimeAggregateRoot : IAggregateRootWithState<ProjectTimeAggregateRootState, int>,
        ICommandHandler<TrackTimeCommand, ProjectTimeAggregateRoot, int>,
        IEventRecipient<ProjectTimeTrackedEvent, ProjectTimeAggregateRoot, int>
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

        public void On(ProjectTimeTrackedEvent @event)
        {
            State.On(@event);
        }
    }
}