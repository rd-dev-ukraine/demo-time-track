using System;
using System.Collections.Generic;
using LanceTrack.Domain.ProjectTime;
using LanceTrack.Domain.TimeTracking;
using LanceTrack.Domain.UserAccounts;
using LanceTrack.Server.Dependencies.TimeTracking.Event;
using LanceTrack.Server.Dependencies.TimeTracking.ReadModels;
using LanceTrack.Server.Projects;
using LanceTrack.Server.Projects.Contract;

namespace LanceTrack.Server.TimeTracking
{
    public class TimeTrackingService : ITimeTrackingService
    {
        private readonly ITimeTrackingEventRepository _eventRepository;
        private readonly IProjectTimeService _projectTimeService;
        private readonly IProjectPermissionsService _projectPermissionsService;
        private readonly List<IProjectTimeReadModel> _readModels; 
        private readonly UserAccount _currentUser;
        private readonly ProjectTimeAggregateRootRepository _repository;

        public TimeTrackingService(
            ITimeTrackingEventRepository eventRepository, 
            IProjectTimeService projectTimeService, 
            IProjectPermissionsService projectPermissionsService,
            List<IProjectTimeReadModel> readModels,
            UserAccount currentUser)
        {
            _eventRepository = eventRepository;
            _projectTimeService = projectTimeService;
            _projectPermissionsService = projectPermissionsService;
            _currentUser = currentUser;
            _readModels = readModels;

            _repository = new ProjectTimeAggregateRootRepository(projectId =>
            {
                var project = _projectTimeService.GetById(projectId);
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