using BLToolkit.DataAccess;
using TypeLite;

namespace LanceTrack.Domain.Invoicing
{
    [TableName("InvoiceDetailsData"), TsClass(Module = "Api")]
    public class InvoiceDetails
    {
        [PrimaryKey]
        public string InvoiceNum { get; set; }

        [PrimaryKey]
        public int UserId { get; set; }

        public decimal UserSum { get; set; }

        public decimal UserHours { get; set; }

        public decimal? UserReceivedSum { get; set; }
    }
}