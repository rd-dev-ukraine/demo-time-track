using System;
using System.Collections.Generic;
using System.Linq;
using LanceTrack.Cqrs.Contract;
using LanceTrack.Server.Cqrs.ProjectTime.Dependencies;
using LanceTrack.Server.Cqrs.ProjectTime.Events;
using LanceTrack.Server.Dependencies.ProjectDailyTime;

namespace LanceTrack.Server.Cqrs.ProjectTime.ReadModels
{
    public class DailyTimeReadModelManager : IAggregateRootReadModelManager<ProjectTimeAggregateRoot, int>, 
        IAggregateRootEventRecipient<ProjectTimeTrackedEvent, ProjectTimeAggregateRoot, int>
    {
        private readonly IDailyTimeStorage _storage;
        private readonly List<ProjectDailyTimeData> _readModels = new List<ProjectDailyTimeData>();

        public DailyTimeReadModelManager(IDailyTimeStorage storage)
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
                dailyTime = new ProjectDailyTimeData
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