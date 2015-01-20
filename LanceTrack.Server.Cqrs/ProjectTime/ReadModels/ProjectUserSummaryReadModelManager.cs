using System;
using System.Collections.Generic;
using System.Linq;
using LanceTrack.Cqrs.Contract;
using LanceTrack.Domain.Projects;
using LanceTrack.Server.Cqrs.Infrastructure;
using LanceTrack.Server.Cqrs.ProjectTime.Dependencies;
using LanceTrack.Server.Cqrs.ProjectTime.Events;
using LanceTrack.Server.Cqrs.ProjectTime.State;

namespace LanceTrack.Server.Cqrs.ProjectTime.ReadModels
{
    public class ProjectUserSummaryReadModelManager : IAggregateRootReadModelManager<ProjectTimeAggregateRoot, int>,
        IReadModelEventRecipient<TimeTrackedEvent, ProjectTimeAggregateRootState, ProjectTimeAggregateRoot, int>,
        IReadModelEventRecipient<InvoiceEvent, ProjectTimeAggregateRootState, ProjectTimeAggregateRoot, int>
    {
        // Key is projectId, userId
        private readonly Dictionary<int, ProjectUserSummary> _models = new Dictionary<int, ProjectUserSummary>();

        private readonly IProjectUserSummaryStorage _storage;

        public ProjectUserSummaryReadModelManager(IProjectUserSummaryStorage storage)
        {
            if (storage == null)
                throw new ArgumentNullException("storage");

            _storage = storage;
        }

        public void On(TimeTrackedEvent @event, ProjectTimeAggregateRootState state)
        {
            RecalculateSummary(@event.UserId, @event.ProjectId, state);
        }

        public void On(InvoiceEvent @event, ProjectTimeAggregateRootState state)
        {
            RecalculateSummary(@event.UserId, @event.ProjectId, state);
        }

        private void RecalculateSummary(int userId, int projectId, ProjectTimeAggregateRootState state)
        {
            var model = _models.GetOrAdd(userId, new ProjectUserSummary
            {
                ProjectId = projectId,
                UserId = userId
            });

            model.ProjectTotalAmountEarned = 0;
            model.ProjectTotalHoursReported = 0;
            model.UserTotalAmountEarned = 0;
            model.UserTotalHoursReported = 0;

            foreach (var dt in state.DailyTime)
            {
                var hoursReported = dt.TotalHours - dt.PaidHours;
                var amountEarned = hoursReported * dt.HourlyRate;

                model.ProjectTotalHoursReported += hoursReported;
                model.ProjectTotalAmountEarned += amountEarned;

                if (dt.UserId == userId)
                {
                    model.UserTotalHoursReported += hoursReported;
                    model.UserTotalAmountEarned += amountEarned;
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