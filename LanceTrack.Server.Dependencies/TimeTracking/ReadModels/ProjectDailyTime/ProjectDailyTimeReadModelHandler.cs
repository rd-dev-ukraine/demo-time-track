using System;
using System.Collections.Generic;
using System.Linq;
using LanceTrack.Server.Dependencies.TimeTracking.Event;
using ProjectDailyTimeEntity = LanceTrack.Server.Dependencies.Project.ProjectDailyTime;

namespace LanceTrack.Server.Dependencies.TimeTracking.ReadModels.ProjectDailyTime
{
    public class ProjectDailyTimeReadModelHandler : IProjectTimeReadModelHandler
    {
        private readonly IProjectDailyTimeStorage _storage;
        private readonly List<ProjectDailyTimeEntity> _readModels = new List<ProjectDailyTimeEntity>();

        public ProjectDailyTimeReadModelHandler(IProjectDailyTimeStorage storage)
        {
            if (storage == null)
                throw new ArgumentNullException("storage");

            _storage = storage;
        }

        public void AppyEvent(ProjectTimeTrackedEvent evt)
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