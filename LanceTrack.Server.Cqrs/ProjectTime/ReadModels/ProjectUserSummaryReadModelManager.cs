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
    public class ProjectUserSummaryReadModelManager : IAggregateRootReadModelManager<ProjectTimeAggregateRoot, int>,
        IReadModelEventRecipient<TimeTrackedEvent, ProjectTimeAggregateRootState, ProjectTimeAggregateRoot, int>
    {
        // Key is projectId, userId
        private readonly Dictionary<Tuple<int, int>, ProjectUserSummary> _models = new Dictionary<Tuple<int, int>, ProjectUserSummary>();

        private readonly IProjectUserSummaryStorage _storage;

        public ProjectUserSummaryReadModelManager(IProjectUserSummaryStorage storage)
        {
            if (storage == null)
                throw new ArgumentNullException("storage");

            _storage = storage;
        }

        public void On(TimeTrackedEvent @event, ProjectTimeAggregateRootState state)
        {
            var key = new Tuple<int, int>(@event.ProjectId, @event.UserId);

            ProjectUserSummary model;
            if (!_models.TryGetValue(key, out model))
                _models.Add(key, model = new ProjectUserSummary
                                {
                                    ProjectId = @event.ProjectId,
                                    UserId = @event.UserId
                                });

            model.ProjectTotalAmountEarned = 0;
            model.ProjectTotalHoursReported = 0;
            model.UserTotalAmountEarned = 0;
            model.UserTotalHoursReported = 0;

            foreach(var dt in state.ProjectUserTime.Where(e => e.ProjectId == @event.ProjectId))
            {
                model.ProjectTotalHoursReported += dt.Hours;
                model.ProjectTotalAmountEarned += dt.Hours * dt.HourlyRate;
                
                if (dt.UserId == @event.UserId)
                {
                    model.UserTotalHoursReported += dt.Hours;
                    model.UserTotalAmountEarned += dt.Hours * dt.HourlyRate;
                }
            }
        }

        public void Save()
        {
            foreach (var model in _models.Values)
                _storage.Save(model);
        }
    }
}