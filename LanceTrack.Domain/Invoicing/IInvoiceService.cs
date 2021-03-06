﻿using System.Collections.Generic;

namespace LanceTrack.Domain.Invoicing
{
    public interface IInvoiceService
    {
        IEnumerable<InvoiceInfo> MyPendingInvoices();

        IEnumerable<InvoiceInfo> Archive();

        InvoiceInfo Get(string number);

        List<InvoiceDetails> Details(string invoiceNumber);

        List<InvoiceRecalculationResult> RecalculateInvoiceInfo(int projectId, List<InvoiceUserRequest> invoiceUserRequest);

        List<InvoiceRecalculationResult> DistributeInvoiceEarnings(int projectId, string invoiceNum, decimal earningsSum);

        void MarkInvoiceAsPaid(int projectId, string invoiceNum);

        void CancelInvoice(int projectId, string invoiceNum);

        /// <summary>
        /// Bill specified project for user for specified amount of hours and returns new invoice number.
        /// </summary>
        string BillProject(int projectId, List<InvoiceUserRequest> invoiceUserRequest);
    }
}