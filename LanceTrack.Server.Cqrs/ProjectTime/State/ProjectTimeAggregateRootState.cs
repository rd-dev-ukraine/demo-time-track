using System;
using System.Collections.Generic;
using System.Linq;
using LanceTrack.Cqrs.Contract;
using LanceTrack.Domain.Projects;
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
        private readonly List<DailyTime> _dailyTime = new List<DailyTime>();
        private readonly List<UserInvoiceInfo> _invoices = new List<UserInvoiceInfo>();
        private readonly List<UserBilling> _billing = new List<UserBilling>();

        public IEnumerable<DailyTime> ProjectUserTime
        {
            get { return _dailyTime; }
        }

        public decimal CalculateInvoiceSum(int userId, decimal hours)
        {
            var sum = 0M;
            var nonBilledHours = hours;

            var dailyTime = _dailyTime.Where(t => t.UserId == userId)
                                      .Where(t => t.TotalHours - t.BilledHours > 0)
                                      .OrderBy(t => t.Date);

            foreach (var time in dailyTime)
            {
                var billingHours = Math.Min(nonBilledHours, time.TotalHours - time.BilledHours);

                sum += billingHours * time.HourlyRate;

                nonBilledHours -= billingHours;

                if (nonBilledHours == 0)
                    break;
            }

            return sum;
        }

        public decimal MaxBillableHours(int userId)
        {
            return _dailyTime.Where(b => b.UserId == userId).SumOrDefault(b => b.TotalHours - b.BilledHours);
        }

        public int InvoiceCount()
        {
            return _invoices.GroupBy(i => i.InvoiceNum).Count();
        }

        public void On(TimeTrackedEvent e)
        {
            var date = Date(e.At);

            if (e.Hours == 0)
            {
                _dailyTime.RemoveAll(t => t.Date == date && t.UserId == e.UserId);
            }
            else
            {
                var dailyTime = _dailyTime.SingleOrDefault(t => t.Date == date && t.UserId == e.UserId);

                if (dailyTime == null)
                {
                    dailyTime = new DailyTime
                    {
                        Date = date,
                        HourlyRate = e.HourlyRate,
                        ProjectId = e.ProjectId,
                        UserId = e.UserId
                    };
                    _dailyTime.Add(dailyTime);
                }

                dailyTime.TotalHours = e.Hours;
            }

            UpdateUserBilling(e.UserId);
        }

        public void On(InvoiceEvent e)
        {
            var at = Date(e.At);

            if (e.EventType == InvoiceEventType.Cancel)
            {
                _invoices.RemoveAll(i => i.UserId == e.UserId &&
                                         i.InvoiceNum == e.InvoiceNum &&
                                         i.At == at);
            }
            else
            {
                var invoice = _invoices.SingleOrDefault(i => i.UserId == e.UserId &&
                                                             i.InvoiceNum == e.InvoiceNum &&
                                                             i.At == at);
                if (invoice == null)
                {
                    invoice = new UserInvoiceInfo
                    {
                        UserId = e.UserId,
                        At = at,
                        InvoiceNum = e.InvoiceNum
                    };
                    _invoices.Add(invoice);
                }

                invoice.Hours = e.Hours;
                invoice.IsPaid = e.EventType == InvoiceEventType.Paid;
            }

            UpdateUserBilling(e.UserId);
        }

        /// <summary>
        ///     Recalculate billable hours for one user.
        /// </summary>
        private void UpdateUserBilling(int userId)
        {
            var result = new List<UserBilling>();

            var dailyTimeToBill = _dailyTime.Where(t => t.UserId == userId).OrderBy(t => t.Date).ToArray();

            // reset hours info
            foreach (var time in dailyTimeToBill)
            {
                time.BilledHours = 0;
                time.PaidHours = 0;
            }

            using (var invoices = _invoices.Where(i => i.UserId == userId).OrderBy(i => i.At).GetEnumerator())
            {
                if (!invoices.MoveNext())
                    return;

                var invoiceHours = invoices.Current.Hours;

                foreach (var time in dailyTimeToBill)
                {
                    var hoursToBill = time.TotalHours;
                    var lastInvoice = false;

                    while (hoursToBill > 0)
                    {
                        var billedHours = Math.Min(hoursToBill, invoiceHours);
                        result.Add(new UserBilling
                        {
                            At = time.Date,
                            Hours = billedHours,
                            InvoiceNum = invoices.Current.InvoiceNum,
                            IsPaid = invoices.Current.IsPaid,
                            Rate = time.HourlyRate,
                            UserId = userId
                        });
                        time.BilledHours += billedHours;
                        time.PaidHours += invoices.Current.IsPaid ? billedHours : 0;

                        hoursToBill -= billedHours;
                        invoiceHours -= billedHours;

                        if (invoiceHours == 0)
                            if (invoices.MoveNext())
                                invoiceHours = invoices.Current.Hours;
                            else
                            {
                                lastInvoice = true;
                                break;
                            }
                    }

                    if (lastInvoice)
                        break;
                }
            }

            // replace user billing with new result
            _billing.RemoveAll(b => b.UserId == userId);
            _billing.AddRange(result);
        }

        private static DateTime Date(DateTimeOffset value)
        {
            return value.ToUniversalTime().Date;
        }

        private class UserInvoiceInfo
        {
            public DateTime At { get; set; }
            public decimal Hours { get; set; }
            public string InvoiceNum { get; set; }
            public bool IsPaid { get; set; }
            public int UserId { get; set; }
        }
    }
}