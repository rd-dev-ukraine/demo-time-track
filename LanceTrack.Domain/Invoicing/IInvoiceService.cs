using System.Collections.Generic;

namespace LanceTrack.Domain.Invoicing
{
    public interface IInvoiceService
    {
        IEnumerable<Invoice> MyPendingInvoices();

        IEnumerable<Invoice> Archive();

        Invoice Get(string number);

        InvoiceRecalculationResult RecalculateInvoiceInfo(int projectId, decimal hours);

        /// <summary>
        /// Bill specified project for user for specified amount of hours and returns new invoice number.
        /// </summary>
        string BillProject(int projectId, decimal hours);
    }
}