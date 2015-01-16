using System;
using System.Collections.Generic;
using System.Linq;
using LanceTrack.Cqrs.Contract;
using LanceTrack.Server.Cqrs.Infrastructure;
using LanceTrack.Server.Cqrs.ProjectTime.Events;

namespace LanceTrack.Server.Cqrs.ProjectTime.State
{
    /// <summary>
    ///     Helper class reacts on events and calculates user daily time.
    /// </summary>
    public class ProjectTimeAggregateRootState : IAggregateRootState<int>,
        IEventRecipient<TimeTrackedEvent, ProjectTimeAggregateRoot, int>,
        IEventRecipient<InvoiceEvent, ProjectTimeAggregateRoot, int>
    {
        private readonly Dictionary<Tuple<int, DateTime>, ProjectUserDailyTimeRecord> _projectUserDailyTimeData = new Dictionary<Tuple<int, DateTime>, ProjectUserDailyTimeRecord>();
        private readonly Dictionary<int, List<UserBillingHours>> _userBillableHours = new Dictionary<int, List<UserBillingHours>>();
        private readonly Dictionary<int, decimal> _userBilledHours = new Dictionary<int, decimal>();

        public ProjectTimeAggregateRootState()
        {
            Invoices = new Dictionary<string, InvoiceInfo>();
        }

        public Dictionary<string, InvoiceInfo> Invoices { get; private set; }

        public int ProjectId { get; private set; }

        public IEnumerable<ProjectUserDailyTimeRecord> ProjectUserTime
        {
            get { return _projectUserDailyTimeData.Values; }
        }

        int IAggregateRootState<int>.AggregateRootId
        {
            get { return ProjectId; }
        }

        public decimal MaxBillableHours(int userId)
        {
            return _userBillableHours.GetOrAdd(userId, new List<UserBillingHours>()).SumOrDefault(h => h.Hours);
        }

        public decimal CalculateInvoiceSum(int userId, decimal hours)
        {
            var sum = 0M;
            var nonBilledHours = hours;

            foreach(var hrs in _userBillableHours.GetOrAdd(userId, new List<UserBillingHours>()))
            {
                if (nonBilledHours < hrs.Hours)
                {
                    sum += hrs.Rate * nonBilledHours;
                    break; 
                }
                else
                {
                    sum += hrs.Rate * hrs.Hours;
                    nonBilledHours -= hrs.Hours;
                }
            }

            return sum;
        }

        public void On(TimeTrackedEvent e)
        {
            ProjectId = e.ProjectId;

            UpdateDailyTime(e);

            UpdateBillableHours(e.UserId);
        }

        public void On(InvoiceEvent e)
        {
            var userHours = _userBilledHours.GetOrAdd(e.UserId);
            if (e.EventType == InvoiceEventType.Billing)
                _userBilledHours[e.UserId] = userHours + e.Hours;

            UpdateBillableHours(e.UserId);

            var invoiceInfo = Invoices.GetOrAdd(e.InvoiceNum, new InvoiceInfo());
            invoiceInfo.BilledAt = e.At;
            invoiceInfo.IsPaid = e.EventType == InvoiceEventType.Paid;
            invoiceInfo.Number = e.InvoiceNum;
        }

        /// <summary>
        ///     Recalculate billable hours for one user.
        /// </summary>
        private void UpdateBillableHours(int userId)
        {
            var userHours = new List<UserBillingHours>();
            var restOfBilledHours = _userBilledHours.GetOrAdd(userId);

            foreach (var hrs in UserBillingHours(userId).ToList())
            {
                restOfBilledHours -= hrs.Hours;

                // There are billed hours 
                if (restOfBilledHours > 0)
                    continue;

                // Add non-billed hours
                if (restOfBilledHours <= 0)
                {
                    hrs.Hours = Math.Abs(restOfBilledHours);
                    restOfBilledHours = 0;
                }

                if (restOfBilledHours == 0)
                    userHours.Add(hrs);
            }


            _userBillableHours[userId] = userHours;
        }

        private void UpdateDailyTime(TimeTrackedEvent e)
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

        private IEnumerable<UserBillingHours> UserBillingHours(int userId)
        {
            var hoursBatches = ProjectUserTime.Where(a => a.UserId == userId)
                                              .OrderBy(a => a.At)
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