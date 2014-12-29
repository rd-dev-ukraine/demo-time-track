using System;
using System.Collections.Generic;
using System.Linq;
using LanceTrack.Domain.ProjectTime;
using LanceTrack.Server.TimeTracking.Events;

namespace LanceTrack.Server.TimeTracking.ReadModels
{
    public class ProjectDailyTimeReadModel : IProjectTimeReadModel
    {
        private readonly IProjectDailyTimeStorage _storage;
        private readonly List<ProjectDailyTime> _readModels = new List<ProjectDailyTime>();

        public ProjectDailyTimeReadModel(IProjectDailyTimeStorage storage)
        {
            if (storage == null)
                throw new ArgumentNullException("storage");

            _storage = storage;
        }

        public void AppyEvent(TimeTrackedEvent evt)
        {
            var dailyTime = _readModels.FirstOrDefault(m => m.Date == evt.At.Date &&
                                                            m.ProjectId == evt.ProjectId &&
                                                            m.UserId == evt.UserId) ??
                                                new ProjectDailyTime
                                                {
                                                    Date = evt.At.Date,
                                                    ProjectId = evt.ProjectId,
                                                    UserId = evt.UserId
                                                };

            dailyTime.TotalHours = evt.Hours;
        }

        public void Save()
        {
            foreach (var readModel in _readModels)
                _storage.SaveProjectDailyTime(readModel);
        }
    }
}