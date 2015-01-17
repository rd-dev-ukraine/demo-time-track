using System;
using System.Collections.Generic;
using System.Linq;
using LanceTrack.Cqrs.Contract;
using LanceTrack.Domain.Projects;
using LanceTrack.Server.Cqrs.ProjectTime.Dependencies;
using LanceTrack.Server.Cqrs.ProjectTime.Events;
using LanceTrack.Server.Cqrs.ProjectTime.State;

namespace LanceTrack.Server.Cqrs.ProjectTime.ReadModels
{
    public class ProjectDailyTimeReadModelManager : IAggregateRootReadModelManager<ProjectTimeAggregateRoot, int>,
        IReadModelEventRecipient<TimeTrackedEvent, ProjectTimeAggregateRootState, ProjectTimeAggregateRoot, int>
    {
        private readonly IDailyTimeStorage _storage;
        private List<ProjectDailyTime> _readModels = new List<ProjectDailyTime>();

        public ProjectDailyTimeReadModelManager(IDailyTimeStorage storage)
        {
            if (storage == null)
                throw new ArgumentNullException("storage");

            _storage = storage;
        }

        public void On(TimeTrackedEvent evt, ProjectTimeAggregateRootState state)
        {
            _readModels = state.ProjectUserTime.ToList();
        }

        public void Save()
        {
            foreach (var readModel in _readModels)
                _storage.SaveProjectDailyTime(readModel);
        }
    }

}