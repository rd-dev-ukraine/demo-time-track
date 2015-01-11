using System;
using System.Collections.Generic;
using LanceTrack.Cqrs.Contract;
using LanceTrack.Server.Cqrs.ProjectTime.Events;

namespace LanceTrack.Server.Cqrs.ProjectTime
{
    /// <summary>
    /// Helper class reacts on events and calculates user daily time.
    /// </summary>
    public class ProjectTimeAggregateRootState : IAggregateRootState<int>,
        IEventRecipient<ProjectTimeTrackedEvent, ProjectTimeAggregateRoot, int>
    {
        private Dictionary<Tuple<int, int, DateTime>, ProjectUserDailyTimeRecord> _projectUserDailyTimeData = new Dictionary<Tuple<int, int, DateTime>, ProjectUserDailyTimeRecord>();

        public IEnumerable<ProjectUserDailyTimeRecord> ProjectUserTime { get { return _projectUserDailyTimeData.Values; } }

        public int ProjectId { get; private set; }

        int IAggregateRootState<int>.AggregateRootId { get { return ProjectId; } }

        public void On(ProjectTimeTrackedEvent e)
        {
            ProjectId = e.ProjectId;

            var date = e.At.ToUniversalTime().Date;

            var key = new Tuple<int, int, DateTime>(e.ProjectId, e.UserId, date);

            ProjectUserDailyTimeRecord record;
            if (!_projectUserDailyTimeData.TryGetValue(key, out record))
                _projectUserDailyTimeData.Add(key, record = new ProjectUserDailyTimeRecord
                    {
                        ProjectId = e.ProjectId,
                        UserId = e.UserId,
                        At = date
                    });

            record.Hours = e.Hours;

            // Use hourly rate from most early date
            // to prevent overriding it on changing later
            if (record.HourlyRateDate == default(DateTimeOffset) ||
                record.HourlyRateDate > e.At)
            {
                record.HourlyRateDate = e.At;
                record.HourlyRate = e.HourlyRate;
            }
        }
    }

    public class ProjectUserDailyTimeRecord
    {
        public int UserId { get; set; }

        public int ProjectId { get; set; }

        public DateTime At { get; set; }

        public decimal Hours { get; set; }

        public decimal HourlyRate { get; set; }

        public DateTimeOffset HourlyRateDate { get; set; }
    }
}