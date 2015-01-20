using TypeLite;

namespace LanceTrack.Domain.Invoicing
{
    [TsClass(Module = "Api")]
    public class InvoiceInfo : Invoice
    {
        public string ProjectTitle { get; set; }

        public string BilledByUserDisplayName { get; set; }
    }
}