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
        private readonly List<Billing> _billing = new List<Billing>();

        public ProjectTimeAggregateRootState()
        {
            Invoices = new List<UserInvoiceInfo>();
            DailyTime = new List<DailyTime>();
        }

        public List<DailyTime> DailyTime { get; private set; }

        public List<UserInvoiceInfo> Invoices { get; private set; }

        public decimal CalculateInvoiceSum(int userId, decimal hours)
        {
            var sum = 0M;
            var nonBilledHours = hours;

            var dailyTime = DailyTime.Where(t => t.UserId == userId)
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
            return DailyTime.Where(b => b.UserId == userId).SumOrDefault(b => b.TotalHours - b.BilledHours);
        }

        public int InvoiceCount()
        {
            return Invoices.GroupBy(i => i.InvoiceNum).Count();
        }

        public void On(TimeTrackedEvent e)
        {
            var date = Date(e.At);

            if (e.Hours == 0)
            {
                DailyTime.RemoveAll(t => t.Date == date && t.UserId == e.UserId);
            }
            else
            {
                var dailyTime = DailyTime.SingleOrDefault(t => t.Date == date && t.UserId == e.UserId);

                if (dailyTime == null)
                {
                    dailyTime = new DailyTime
                    {
                        Date = date,
                        HourlyRate = e.HourlyRate,
                        ProjectId = e.ProjectId,
                        UserId = e.UserId
                    };
                    DailyTime.Add(dailyTime);
                }

                dailyTime.TotalHours = e.Hours;
            }

            UpdateUserBilling(e.UserId);
        }

        public void On(InvoiceEvent e)
        {
            if (e.EventType == InvoiceEventType.Billing)
            {
                var at = Date(e.At);

                if (e.EventType == InvoiceEventType.Cancel)
                {
                    Invoices.RemoveAll(i => i.UserId == e.UserId &&
                                             i.InvoiceNum == e.InvoiceNum &&
                                             i.At == at);
                }
                else
                {
                    var invoice = Invoices.SingleOrDefault(i => i.UserId == e.UserId &&
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
                        Invoices.Add(invoice);
                    }

                    invoice.Hours = e.Hours;
                    invoice.IsPaid = e.EventType == InvoiceEventType.Paid;
                    invoice.Sum = e.InvoiceSum;
                }

                UpdateUserBilling(e.UserId);
            }
        }

        /// <summary>
        ///     Recalculate billable hours for one user.
        /// </summary>
        private void UpdateUserBilling(int userId)
        {
            var result = new List<Billing>();

            var dailyTimeToBill = DailyTime.Where(t => t.UserId == userId).OrderBy(t => t.Date).ToArray();

            // reset hours info
            foreach (var time in dailyTimeToBill)
            {
                time.BilledHours = 0;
                time.PaidHours = 0;
            }

            using (var invoices = Invoices.Where(i => i.UserId == userId).OrderBy(i => i.At).GetEnumerator())
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
                        result.Add(new Billing
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
            return value.Date;
        }
    }
}