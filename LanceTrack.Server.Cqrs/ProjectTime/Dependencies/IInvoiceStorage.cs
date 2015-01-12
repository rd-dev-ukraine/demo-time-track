using LanceTrack.Domain.Invoicing;

namespace LanceTrack.Server.Cqrs.ProjectTime.Dependencies
{
    public interface IInvoiceStorage
    {
        void Save(Invoice invoice);
    }
}