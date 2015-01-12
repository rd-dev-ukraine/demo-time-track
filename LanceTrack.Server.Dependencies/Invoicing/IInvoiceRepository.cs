using System.Collections.Generic;
using LanceTrack.Domain.Invoicing;

namespace LanceTrack.Server.Dependencies.Invoicing
{
    public interface IInvoiceRepository
    {
        IEnumerable<Invoice> UserPendingInvoices(int userId);

        IEnumerable<Invoice> UserArchiveInvoices(int userId);

        Invoice GetByNumber(string invoiceNumber, int userId);
    }
}