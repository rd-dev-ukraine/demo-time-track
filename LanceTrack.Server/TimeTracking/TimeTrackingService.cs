using System;
using System.Collections.Generic;
using LanceTrack.Domain.TimeTracking;
using LanceTrack.Domain.UserAccounts;
using LanceTrack.Server.Dependencies.TimeTracking.Event;
using LanceTrack.Server.Dependencies.TimeTracking.ReadModels;
using LanceTrack.Server.Projects;

namespace LanceTrack.Server.TimeTracking
{
    public class TimeTrackingService : ITimeTrackingService
    {
        private readonly UserAccount _currentUser;
        private readonly ITimeTrackingEventRepository _eventRepository;
        private readonly ProjectService _projectService;
        private readonly List<IProjectTimeReadModelHandler> _readModels;
        private readonly ProjectTimeAggregateRootRepository _repository;

        public TimeTrackingService(
            ITimeTrackingEventRepository eventRepository,
            ProjectService projectService,
            List<IProjectTimeReadModelHandler> readModels,
            UserAccount currentUser)
        {
            _eventRepository = eventRepository;
            _projectService = projectService;
            _currentUser = currentUser;
            _readModels = readModels;

            _repository = new ProjectTimeAggregateRootRepository(projectId =>
            {
                var project = _projectService.GetById(projectId);
                var result = new ProjectTimeAggregateRoot(_projectService, project, _readModels);

                foreach (var e in _eventRepository.ReadTimeTrackedEvents(projectId))
                    result.Apply(e);

                return result;
            });
        }

        public void TrackTime(int projectId, int userId, DateTime at, decimal hours)
        {
            var aggregateRoot = _repository.GetAggregateRoot(projectId);
            aggregateRoot.TrackTime(_currentUser.Id, userId, at, hours);
            aggregateRoot.Save(_eventRepository);
        }
    }
}