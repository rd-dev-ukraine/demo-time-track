using System;
using System.Collections.Generic;
using System.Linq;
using LanceTrack.Cqrs.Contract;
using LanceTrack.Domain.ProjectUserInfo;
using LanceTrack.Server.Cqrs.ProjectTime.Dependencies;
using LanceTrack.Server.Cqrs.ProjectTime.Events;

namespace LanceTrack.Server.Cqrs.ProjectTime.ReadModels
{
    public class ProjectUserSummaryReadModelManager : IAggregateRootReadModelManager<ProjectTimeAggregateRoot, int>,
        IAggregateRootEventRecipient<ProjectTimeTrackedEvent, ProjectTimeAggregateRoot, int>
    {
        // Key is projectId, userId
        private readonly Dictionary<Tuple<int, int>, ProjectUserSummaryData> _models = new Dictionary<Tuple<int, int>, ProjectUserSummaryData>();
        private readonly HashSet<DailyTime> _dailyTime = new HashSet<DailyTime>();

        private readonly IProjectUserSummaryStorage _storage;

        public ProjectUserSummaryReadModelManager(IProjectUserSummaryStorage storage)
        {
            if (storage == null)
                throw new ArgumentNullException("storage");

            _storage = storage;
        }

        public void On(ProjectTimeTrackedEvent @event)
        {
            var entity = new DailyTime
            {
                At = @event.At.ToUniversalTime().Date,
                ProjectId = @event.ProjectId,
                UserId = @event.UserId,
                Hours = @event.Hours,
                HourlyRate = @event.HourlyRate
            };

            _dailyTime.Remove(entity);
            _dailyTime.Add(entity);

            var key = new Tuple<int, int>(@event.ProjectId, @event.UserId);

            ProjectUserSummaryData model;
            if (!_models.TryGetValue(key, out model))
                _models.Add(key, model = new ProjectUserSummaryData
                                {
                                    ProjectId = @event.ProjectId,
                                    UserId = @event.UserId
                                });

            model.ProjectTotalAmountEarned = 0;
            model.ProjectTotalHoursReported = 0;
            model.UserTotalAmountEarned = 0;
            model.UserTotalHoursReported = 0;

            foreach(var dt in _dailyTime.Where(e => e.ProjectId == @event.ProjectId))
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

        private class DailyTime : IEquatable<DailyTime>
        {
            public DateTime At { get; set; }

            public decimal Hours { get; set; }

            public int ProjectId { get; set; }

            public int UserId { get; set; }

            public decimal HourlyRate { get; set; }

            public bool Equals(DailyTime other)
            {
                if (ReferenceEquals(null, other))
                    return false;
                if (ReferenceEquals(this, other))
                    return true;

                return At.Equals(other.At) &&
                       ProjectId == other.ProjectId &&
                       UserId == other.UserId;
            }

            public override bool Equals(object obj)
            {
                if (ReferenceEquals(null, obj))
                    return false;
                if (ReferenceEquals(this, obj))
                    return true;
                if (obj.GetType() != GetType())
                    return false;

                return Equals((DailyTime)obj);
            }

            public override int GetHashCode()
            {
                unchecked
                {
                    var hashCode = At.GetHashCode();
                    hashCode = (hashCode * 397) ^ ProjectId;
                    hashCode = (hashCode * 397) ^ UserId;
                    return hashCode;
                }
            }
        }
    }
}