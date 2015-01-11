using System;
using System.Collections.Generic;
using System.Linq;
using LanceTrack.Cqrs.Contract;
using LanceTrack.Server.Cqrs.Infrastructure;
using LanceTrack.Server.Cqrs.ProjectTime.Events;

namespace LanceTrack.Server.Cqrs.ProjectTime.State
{
    /// <summary>
    /// Helper class reacts on events and calculates user daily time.
    /// </summary>
    public class ProjectTimeAggregateRootState : IAggregateRootState<int>,
        IEventRecipient<ProjectTimeTrackedEvent, ProjectTimeAggregateRoot, int>,
        IEventRecipient<InvoiceEvent, ProjectTimeAggregateRoot, int>
    {
        private readonly Dictionary<Tuple<int, DateTime>, ProjectUserDailyTimeRecord> _projectUserDailyTimeData = new Dictionary<Tuple<int, DateTime>, ProjectUserDailyTimeRecord>();
        private readonly Dictionary<int, List<UserBillingHours>> _userBillingHours = new Dictionary<int, List<UserBillingHours>>();

        public int ProjectId { get; private set; }

        int IAggregateRootState<int>.AggregateRootId { get { return ProjectId; } }

        public IEnumerable<ProjectUserDailyTimeRecord> ProjectUserTime { get { return _projectUserDailyTimeData.Values; } }

        public void On(ProjectTimeTrackedEvent e)
        {
            ProjectId = e.ProjectId;

            UpdateDailyTime(e);
            UpdateBillableHours(e);
        }

        public void On(InvoiceEvent e)
        {
            throw new NotImplementedException();
        }

        private void UpdateDailyTime(ProjectTimeTrackedEvent e)
        {
            var date = e.At.ToUniversalTime().Date;

            var key = new Tuple<int, DateTime>(e.UserId, date);

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

        /// <summary>
        /// Recalculate billable hours for one user.
        /// </summary>
        private void UpdateBillableHours(ProjectTimeTrackedEvent e)
        {
            _userBillingHours[e.UserId] = UserBillingHours(e.UserId).ToList();
        }

        private IEnumerable<UserBillingHours> UserBillingHours(int userId)
        {
            var hoursBatches = ProjectUserTime.Where(a => a.UserId == userId).OrderBy(a => a.At)
                                    .Batch(a => a.HourlyRate)
                                    .ToArray();

            return hoursBatches.Select(a => new UserBillingHours
                                    {
                                        Rate = a.Max(r => r.HourlyRate),
                                        UserId = userId,
                                        Hours = a.Sum(r => r.Hours)
                                    });
        }
    }
}