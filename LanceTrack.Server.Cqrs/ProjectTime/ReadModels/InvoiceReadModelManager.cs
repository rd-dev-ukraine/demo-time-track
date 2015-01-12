using System;
using System.Collections.Generic;
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

        public InvoiceReadModelManager(IInvoiceStorage invoiceStorage)
        {
            if (invoiceStorage == null)
                throw new ArgumentNullException("invoiceStorage");

            _invoiceStorage = invoiceStorage;
        }

        public void Save()
        {
            foreach (var invoice in _invoices.Values)
                _invoiceStorage.Save(invoice);
        }

        public void On(InvoiceEvent e, ProjectTimeAggregateRootState state)
        {
            var invoice = _invoices.GetOrAdd(e.InvoiceNum);

            invoice.At = e.At;
            invoice.Hours = e.Hours;
            invoice.InvoiceNum = e.InvoiceNum;
            invoice.IsPaid = invoice.IsPaid || e.EventType == InvoiceEventType.Paid;
            invoice.ProjectId = e.ProjectId;
            invoice.Sum = e.InvoiceSum;
            invoice.UserId = e.UserId;
        }
    }
}