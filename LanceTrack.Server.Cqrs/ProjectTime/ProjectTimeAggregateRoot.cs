using System;
using System.Collections.Generic;
using System.Linq;
using LanceTrack.Cqrs.Contract;
using LanceTrack.Domain.TimeTracking;
using LanceTrack.Server.Dependencies.Project;

namespace LanceTrack.Server.Cqrs.ProjectTime
{
    public class ProjectTimeAggregateRoot : IAggregateRoot<int>,
        IAggregateRootCommandHandler<TrackTimeCommand, ProjectTimeAggregateRoot, int>,
        IAggregateRootEventRecipient<ProjectTimeTrackedEvent, ProjectTimeAggregateRoot, int>
    {
        private readonly IProjectService _projectService;
        private State _state;

        public ProjectTimeAggregateRoot(IProjectService projectService)
        {
            if (projectService == null)
                throw new ArgumentNullException("projectService");

            _projectService = projectService;
        }

        public IEnumerable<IEvent<ProjectTimeAggregateRoot, int>> Execute(TrackTimeCommand command)
        {
            if (_state.ProjectId != command.ProjectId)
                throw new ArgumentException("Command belongs to another project.");

            var project = _projectService.GetById(command.ProjectId);

                if (project.Status != ProjectStatus.Active)
                    throw new ProjectNotReportableException();

            var perms = _projectService.CalculatePermissions(command.UserId, project.Id);

            if (perms < ProjectPermissions.TrackSelf)
                throw new ProjectAuthorizationException();
            
            if (project.StartDate.Date > command.At.Date)
                throw new ProjectNotReportableException();
            if (project.EndDate != null && project.EndDate < command.At.Date)
                throw new ProjectNotReportableException();

            if (project.MaxTotalHoursPerDay != null &&
                command.Hours > project.MaxTotalHoursPerDay.Value)
                throw new IncorrectHoursException();


            _state = _state ?? new State();

            if (project.MaxTotalHours != null &&
                (_state.ProjectTotalHours + command.Hours) > project.MaxTotalHours)
                throw new IncorrectHoursException();

            var @event = new ProjectTimeTrackedEvent
            {
                At = command.At,
                Hours = command.Hours,
                ProjectId = project.Id,
                RegisteredAt = DateTimeOffset.Now,
                RegisteredByUserId = command.UserId,
                UserId = command.UserId
            };

            yield return @event;
        }

        public void On(ProjectTimeTrackedEvent @event)
        {
            if (_state == null)
                _state = new State();

            _state.ProjectId = @event.ProjectId;

            var userTimeRecord = _state.UserTime.SingleOrDefault(r => r.UserId == @event.UserId && r.Date == @event.At.Date);
            if (userTimeRecord == null)
                _state.UserTime.Add(userTimeRecord = new UserTimeRecord
                {
                    Date = @event.At.Date,
                    UserId = @event.UserId
                });

            userTimeRecord.TotalHours = @event.Hours;
        }

        public class State
        {
            public State()
            {
                UserTime = new List<UserTimeRecord>();
            }

            public int ProjectId { get; set; }

            public decimal ProjectTotalHours
            {
                get { return UserTime.Any() ? UserTime.Sum(t => t.TotalHours) : 0; }
            }

            public List<UserTimeRecord> UserTime { get; set; }
        }

        public class UserTimeRecord
        {
            public DateTimeOffset Date { get; set; }
            public decimal TotalHours { get; set; }
            public int UserId { get; set; }
        }
    }
}