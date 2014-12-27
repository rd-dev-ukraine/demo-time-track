using System;
using System.Collections.Generic;
using LanceTrack.Domain.TimeTracking;
using LanceTrack.Domain.UserAccounts;
using LanceTrack.Server.Projects;

namespace LanceTrack.Server.TimeTracking
{
    public class TimeTrackingService : ITimeTrackingService
    {
        private readonly ITimeTrackingEventRepository _eventRepository;
        private readonly IProjectService _projectService;
        private readonly IProjectPermissionsService _projectPermissionsService;
        private readonly UserAccount _currentUser;

        private static readonly Dictionary<int, ProjectTimeAggregateRoot> _aggregateRoots = new Dictionary<int, ProjectTimeAggregateRoot>(); 

        public TimeTrackingService(
            ITimeTrackingEventRepository eventRepository, 
            IProjectService projectService, 
            IProjectPermissionsService projectPermissionsService,
            UserAccount currentUser)
        {
            _eventRepository = eventRepository;
            _projectService = projectService;
            _projectPermissionsService = projectPermissionsService;
            _currentUser = currentUser;
        }


        public void TrackTime(int projectId, int userId, DateTime at, decimal hours)
        {
            if (!_aggregateRoots.ContainsKey(projectId))
            {
                var project = _projectService.GetById(projectId);
                if (project == null)
                    throw new ArgumentNullException("projectId");

                var root = new ProjectTimeAggregateRoot(_projectPermissionsService, _eventRepository, project);

                foreach (var e in _eventRepository.ReadEvents(projectId))
                    root.Apply(e);

                _aggregateRoots[projectId] = root;
            }

            var aggregateRoot = _aggregateRoots[projectId];
            aggregateRoot.TrackTime(_currentUser.Id, userId, at, hours);
        }
    }
}