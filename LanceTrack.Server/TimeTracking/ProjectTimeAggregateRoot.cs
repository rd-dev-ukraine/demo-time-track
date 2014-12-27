using System;
using System.Collections.Generic;
using System.Linq;
using LanceTrack.Domain.Projects;
using LanceTrack.Domain.TimeTracking;
using LanceTrack.Server.Projects;
using LanceTrack.Server.TimeTracking.Events;

namespace LanceTrack.Server.TimeTracking
{
    /// <summary>
    /// Process time tracking and invoicing commands from user and accepts time tracked, invoice sent and invoice paid events.
    /// </summary>
    public class ProjectTimeAggregateRoot
    {
        private readonly IProjectPermissionsService _projectPermissionsService;
        private readonly ITimeTrackingEventRepository _eventRepository;
        private readonly Project _project;

        public ProjectTimeAggregateRoot(
            IProjectPermissionsService projectPermissionsService, 
            ITimeTrackingEventRepository eventRepository, 
            Project project)
        {
            if (eventRepository == null)
                throw new ArgumentNullException("eventRepository");

            if (project == null)
                throw new ArgumentNullException("project");
            if (projectPermissionsService == null)
                throw new ArgumentNullException("projectPermissionsService");

            _project = project;
            _eventRepository = eventRepository;
            _projectPermissionsService = projectPermissionsService;
        }

        private State _state;

        public void TrackTime(int currentUserId, int trackForUserId, DateTimeOffset at, decimal hours)
        {
            if (_project.Status != ProjectStatus.Active)
                throw new ProjectNotReportableException();

            var perms = _projectPermissionsService.CalculatePermissions(currentUserId, _project.Id);
            if (perms < ProjectPermissions.TrackSelf)
                throw new ProjectAuthorizationException();
            if (currentUserId != trackForUserId &&
                perms < ProjectPermissions.TrackAsOtherUser)
                throw new ProjectAuthorizationException();

            if (_project.StartTime.Date > at.Date)
                throw new ProjectNotReportableException();
            if (_project.EndTime != null && _project.EndTime < at.Date)
                throw new ProjectNotReportableException();

            if (_project.MaxTotalHoursPerDay != null &&
                hours > _project.MaxTotalHoursPerDay.Value)
                throw new IncorrectHoursException();


            if (_state == null)
                throw new ArgumentNullException("State is not initialized.");

            if (_project.MaxTotalHours != null &&
                (_state.ProjectTotalHours + hours) > _project.MaxTotalHours)
                throw new IncorrectHoursException();

            var @event = new TimeTrackedEvent
            {
                At = at,
                Hours = hours,
                ProjectId = _project.Id,
                RegisteredAt = DateTimeOffset.Now,
                RegisteredByUserId = currentUserId,
                UserId = trackForUserId
            };
            _eventRepository.StoreEvent(@event);
        }

        public void Apply(TimeTrackedEvent @event)
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
        }

        public class State
        {
            public State()
            {
                UserTime = new List<UserTimeRecord>();
            }

            public decimal ProjectTotalHours
            {
                get
                {
                    return UserTime.Any() ? UserTime.Sum(t => t.TotalHours) : 0;
                }
            }

            public List<UserTimeRecord> UserTime { get; set; }
        }

        public class UserTimeRecord
        {
            public DateTimeOffset Date { get; set; }

            public int UserId { get; set; }

            public decimal TotalHours { get; set; }
        }
    }
}