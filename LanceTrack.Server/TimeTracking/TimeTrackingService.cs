using System;
using System.Collections.Generic;
using LanceTrack.Domain.TimeTracking;
using LanceTrack.Domain.UserAccounts;
using LanceTrack.Server.Projects;
using LanceTrack.Server.TimeTracking.Events;
using LanceTrack.Server.TimeTracking.ReadModels;

namespace LanceTrack.Server.TimeTracking
{
    public class TimeTrackingService : ITimeTrackingService
    {
        private readonly ITimeTrackingEventRepository _eventRepository;
        private readonly IProjectService _projectService;
        private readonly IProjectPermissionsService _projectPermissionsService;
        private readonly List<IProjectTimeReadModel> _readModels; 
        private readonly UserAccount _currentUser;
        private readonly ProjectTimeAggregateRootRepository _repository;

        public TimeTrackingService(
            ITimeTrackingEventRepository eventRepository, 
            IProjectService projectService, 
            IProjectPermissionsService projectPermissionsService,
            List<IProjectTimeReadModel> readModels,
            UserAccount currentUser)
        {
            _eventRepository = eventRepository;
            _projectService = projectService;
            _projectPermissionsService = projectPermissionsService;
            _currentUser = currentUser;
            _readModels = readModels;

            _repository = new ProjectTimeAggregateRootRepository(projectId =>
            {
                var project = _projectService.GetById(projectId);
                var result = new ProjectTimeAggregateRoot(_projectPermissionsService, project, _readModels);

                foreach (var e in _eventRepository.ReadTimeTrackedEvents(projectId))
                    result.Apply(e);

                return result;
            });
        }


        public void TrackTime(int projectId, int userId, DateTime at, decimal hours)
        {
            var aggregateRoot = _repository.GetAggregateRoot(projectId);
            aggregateRoot.TrackTime(_currentUser.Id, userId, at, hours);


        }
    }
}