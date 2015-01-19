using System;
using System.Collections.Generic;
using System.Linq;
using LanceTrack.Cqrs.Contract;
using LanceTrack.Domain.Invoicing;
using LanceTrack.Server.Cqrs.Infrastructure;
using LanceTrack.Server.Cqrs.ProjectTime.Dependencies;
using LanceTrack.Server.Cqrs.ProjectTime.Events;
using LanceTrack.Server.Cqrs.ProjectTime.State;

namespace LanceTrack.Server.Cqrs.ProjectTime.ReadModels
{
    public class InvoiceReadModelManager : IAggregateRootReadModelManager<ProjectTimeAggregateRoot, int>,
        IReadModelEventRecipient<InvoiceEvent, ProjectTimeAggregateRootState, ProjectTimeAggregateRoot, int>
    {
        private readonly IInvoiceStorage _invoiceStorage;
        private readonly Dictionary<string, Invoice> _invoices = new Dictionary<string, Invoice>();
        private readonly Dictionary<string, Dictionary<int, InvoiceDetails>> _invoiceDetails = new Dictionary<string, Dictionary<int, InvoiceDetails>>();

        public InvoiceReadModelManager(IInvoiceStorage invoiceStorage)
        {
            if (invoiceStorage == null)
                throw new ArgumentNullException("invoiceStorage");

            _invoiceStorage = invoiceStorage;
        }

        public void Save()
        {
            foreach (var invoice in _invoices.Values)
                _invoiceStorage.Save(
                    invoice, 
                    _invoiceDetails.GetOrDefault(invoice.InvoiceNum, new Dictionary<int, InvoiceDetails>()).Values.ToList());
        }

        public void On(InvoiceEvent e, ProjectTimeAggregateRootState state)
        {
            if (e.EventType == InvoiceEventType.Billing)
                OnBilling(e);
            if (e.EventType == InvoiceEventType.EarningDistribution)
                OnDistributeEarnings(e);
        }

        private void OnBilling(InvoiceEvent e)
        {
            var details = GetOrCreateInvoiceDetails(e);

            details.InvoiceNum = e.InvoiceNum;
            details.UserHours = e.Hours;
            details.UserId = e.UserId;
            details.UserSum = e.InvoiceSum;

            var invoice = _invoices.GetOrAdd(e.InvoiceNum, new Invoice());
            invoice.At = e.RegisteredAt;
            invoice.BilledByUserId = e.RegisteredByUserId;
            invoice.ProjectId = e.ProjectId;
            invoice.InvoiceNum = e.InvoiceNum;

            RecalculateInvoice(e.InvoiceNum);
        }

        private void OnDistributeEarnings(InvoiceEvent e)
        {
            var details = GetOrCreateInvoiceDetails(e);
            details.UserReceivedSum = e.InvoiceSum;

            RecalculateInvoice(e.InvoiceNum);
        }

        private void RecalculateInvoice(string invoiceNum)
        {
            var details = _invoiceDetails.GetOrDefault(invoiceNum, new Dictionary<int, InvoiceDetails>()).Values;
            if (details.Any())
            {
                var invoice = _invoices[invoiceNum];

                invoice.Hours = details.Sum(d => d.UserHours);
                invoice.Sum = details.Sum(d => d.UserSum);
                invoice.ReceivedSum = details.Sum(d => d.UserReceivedSum);
            }
        }

        private InvoiceDetails GetOrCreateInvoiceDetails(InvoiceEvent e)
        {
            var detailList = _invoiceDetails.GetOrAdd(e.InvoiceNum, new Dictionary<int, InvoiceDetails>());
            var details = detailList.GetOrAdd(e.UserId, new InvoiceDetails());
            return details;
        }
    }
}