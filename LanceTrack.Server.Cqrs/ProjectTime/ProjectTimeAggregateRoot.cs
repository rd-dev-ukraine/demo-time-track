﻿using System;
using System.Collections.Generic;
using System.Linq;
using LanceTrack.Cqrs.Contract;
using LanceTrack.Domain.TimeTracking;
using LanceTrack.Server.Cqrs.ProjectTime.Commands;
using LanceTrack.Server.Cqrs.ProjectTime.Events;
using LanceTrack.Server.Dependencies.Project;

namespace LanceTrack.Server.Cqrs.ProjectTime
{
    public class ProjectTimeAggregateRoot : IAggregateRoot<int>,
        IAggregateRootCommandHandler<TrackTimeCommand, ProjectTimeAggregateRoot, int>,
        IAggregateRootEventRecipient<ProjectTimeTrackedEvent, ProjectTimeAggregateRoot, int>
    {
        private readonly IProjectService _projectService;
        private readonly State _state = new State();

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
        }

        public IEnumerable<IReadModelManager<int>> ReadModels { get; private set; }

        public IEnumerable<IEvent<ProjectTimeAggregateRoot, int>> Execute(TrackTimeCommand command)
        {
            if (_state.IsAnyEventProcessed && _state.ProjectId != command.ProjectId)
                throw new ArgumentException("Command belongs to another project.");

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

            if (project.MaxTotalHours != null &&
                (_state.ProjectTotalHours + command.Hours) > project.MaxTotalHours)
                throw new IncorrectHoursException();

            if (command.Hours == 0)
                yield break;

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
            _state.IsAnyEventProcessed = true;
            _state.ProjectId = @event.ProjectId;

            var date = @event.At.Date.ToUniversalTime();

            var userTimeRecord = _state.UserTime.SingleOrDefault(r => r.UserId == @event.UserId && r.Date == date);
            if (userTimeRecord == null)
                _state.UserTime.Add(userTimeRecord = new UserTimeRecord
                {
                    Date = date,
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

            public bool IsAnyEventProcessed { get; set; }

            public int ProjectId { get; set; }

            public decimal ProjectTotalHours
            {
                get { return UserTime.Any() ? UserTime.Sum(t => t.TotalHours) : 0; }
            }

            public List<UserTimeRecord> UserTime { get; set; }
        }

        public class UserTimeRecord
        {
            public DateTime Date { get; set; }

            public decimal TotalHours { get; set; }

            public int UserId { get; set; }
        }
    }
}