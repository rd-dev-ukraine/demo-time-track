using System.Collections.Generic;
using LanceTrack.Domain.Invoicing;

namespace LanceTrack.Server.Dependencies.Invoicing
{
    public interface IInvoiceRepository
    {
        IEnumerable<InvoiceInfo> UserPendingInvoices(int userId);

        IEnumerable<InvoiceInfo> UserArchiveInvoices(int userId);

        InvoiceInfo GetByNumber(string invoiceNumber, int userId);

        List<InvoiceDetails> Details(string invoiceNumber, int userId);
    }
}