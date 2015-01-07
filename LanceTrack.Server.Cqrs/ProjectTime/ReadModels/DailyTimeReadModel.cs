using System;
using System.Collections.Generic;
using System.Linq;
using LanceTrack.Cqrs.Contract;
using LanceTrack.Server.Cqrs.ProjectTime.Dependencies;
using LanceTrack.Server.Cqrs.ProjectTime.Events;
using ProjectDailyTimeEntity = LanceTrack.Server.Dependencies.ProjectDailyTime.ProjectDailyTime;

namespace LanceTrack.Server.Cqrs.ProjectTime.ReadModels
{
    public class DailyTimeReadModel : IAggregateRootReadModelManager<ProjectTimeAggregateRoot, int>, 
        IAggregateRootEventRecipient<ProjectTimeTrackedEvent, ProjectTimeAggregateRoot, int>
    {
        private readonly IProjectDailyTimeStorage _storage;
        private readonly List<ProjectDailyTimeEntity> _readModels = new List<ProjectDailyTimeEntity>();

        public DailyTimeReadModel(IProjectDailyTimeStorage storage)
        {
            if (storage == null)
                throw new ArgumentNullException("storage");

            _storage = storage;
        }

        public void On(ProjectTimeTrackedEvent evt)
        {
            var dailyTime = _readModels.FirstOrDefault(m => m.Date == evt.At.Date &&
                                                            m.ProjectId == evt.ProjectId &&
                                                            m.UserId == evt.UserId);

            if (dailyTime == null)
            {
                dailyTime = new ProjectDailyTimeEntity
                {
                    Date = evt.At.Date,
                    ProjectId = evt.ProjectId,
                    UserId = evt.UserId
                };
                _readModels.Add(dailyTime);
            }

            dailyTime.TotalHours = evt.Hours;
        }

        public void Save()
        {
            foreach (var readModel in _readModels)
                _storage.SaveProjectDailyTime(readModel);
        }
    }

}