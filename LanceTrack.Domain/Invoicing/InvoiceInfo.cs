using LinqToDB.Mapping;
using TypeLite;

namespace LanceTrack.Domain.Invoicing
{
    [TsClass(Module = "Api")]
    public class InvoiceInfo : Invoice
    {
        [Column]
        public string ProjectTitle { get; set; }

        [Column]
        public string BilledByUserDisplayName { get; set; }
    }
}