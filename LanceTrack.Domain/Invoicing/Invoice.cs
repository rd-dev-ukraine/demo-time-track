using System;
using BLToolkit.DataAccess;
using BLToolkit.Mapping;
using TypeLite;

namespace LanceTrack.Domain.Invoicing
{
    [TableName("InvoiceData"), TsClass(Module = "Api")]
    public class Invoice
    {
        [PrimaryKey]
        public string InvoiceNum { get; set; }

        public int ProjectId { get; set; }

        public DateTimeOffset At { get; set; }

        public bool IsPaid { get; set; }

        [MapField("InvoiceTotalSum")]
        public decimal Sum { get; set; }

        [MapField("InvoiceTotalHours")]
        public decimal Hours { get; set; }

        public decimal? ReceivedSum { get; set; }

        public int BilledByUserId { get; set; }
    }
}