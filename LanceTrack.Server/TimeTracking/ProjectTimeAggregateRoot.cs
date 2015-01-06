using System;
using System.Collections.Generic;
using System.Linq;
using LanceTrack.Domain.TimeTracking;
using LanceTrack.Server.Dependencies.Project;
using LanceTrack.Server.Dependencies.TimeTracking.Event;
using LanceTrack.Server.Dependencies.TimeTracking.ReadModels;
using LanceTrack.Server.Projects;

namespace LanceTrack.Server.TimeTracking
{
    /// <summary>
    ///     Process time tracking and invoicing commands from user and accepts time tracked, invoice sent and invoice paid
    ///     events.
    /// </summary>
    public class ProjectTimeAggregateRoot
    {
        private readonly Project _project;
        private readonly ProjectService _projectService;
        private readonly List<IProjectTimeReadModelHandler> _readModels;
        private State _state;
        private readonly List<ProjectTimeTrackedEvent> _timeTrackedEvents = new List<ProjectTimeTrackedEvent>();

        public ProjectTimeAggregateRoot(
            ProjectService projectService,
            Project project,
            List<IProjectTimeReadModelHandler> readModels)
        {
            if (project == null)
                throw new ArgumentNullException("project");
            if (projectService == null)
                throw new ArgumentNullException("projectService");
            if (readModels == null)
                throw new ArgumentNullException("readModels");

            _project = project;
            _projectService = projectService;
            _readModels = readModels;

            Events = new List<object>();
        }

        public List<object> Events { get; private set; }

        public void Apply(ProjectTimeTrackedEvent @event)
        {
            if (_state == null)
                _state = new State();

            var userTimeRecord = _state.UserTime.SingleOrDefault(r => r.UserId == @event.UserId && r.Date == @event.At.Date);
            if (userTimeRecord == null)
                _state.UserTime.Add(userTimeRecord = new UserTimeRecord
                {
                    Date = @event.At.Date,
                    UserId = @event.UserId
                });

            userTimeRecord.TotalHours = @event.Hours;

            foreach (var rm in _readModels)
                rm.AppyEvent(@event);
        }

        public void Save(ITimeTrackingEventRepository repository)
        {
            foreach (var evt in _timeTrackedEvents)
                repository.StoreEvent(evt);

            foreach (var rm in _readModels)
                rm.Save();

            _timeTrackedEvents.Clear();
        }

        public void TrackTime(int currentUserId, int trackForUserId, DateTimeOffset at, decimal hours)
        {
            if (_project.Status != ProjectStatus.Active)
                throw new ProjectNotReportableException();

            var perms = _projectService.CalculatePermissions(currentUserId, _project.Id);
            if (perms < ProjectPermissions.TrackSelf)
                throw new ProjectAuthorizationException();
            if (currentUserId != trackForUserId &&
                perms < ProjectPermissions.TrackAsOtherUser)
                throw new ProjectAuthorizationException();

            if (_project.StartDate.Date > at.Date)
                throw new ProjectNotReportableException();
            if (_project.EndDate != null && _project.EndDate < at.Date)
                throw new ProjectNotReportableException();

            if (_project.MaxTotalHoursPerDay != null &&
                hours > _project.MaxTotalHoursPerDay.Value)
                throw new IncorrectHoursException();


            _state = _state ?? new State();

            if (_project.MaxTotalHours != null &&
                (_state.ProjectTotalHours + hours) > _project.MaxTotalHours)
                throw new IncorrectHoursException();

            var @event = new ProjectTimeTrackedEvent
            {
                At = at,
                Hours = hours,
                ProjectId = _project.Id,
                RegisteredAt = DateTimeOffset.Now,
                RegisteredByUserId = currentUserId,
                UserId = trackForUserId
            };

            Apply(@event);

            _timeTrackedEvents.Add(@event);
        }

        public class State
        {
            public State()
            {
                UserTime = new List<UserTimeRecord>();
            }

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