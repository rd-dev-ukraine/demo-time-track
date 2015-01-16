using TypeLite;

namespace LanceTrack.Domain.Invoicing
{
    [TsClass(Module = "Api")]
    public class InvoiceUserRequest
    {
        public int UserId { get; set; }

        public decimal Hours { get; set; }
    }
}