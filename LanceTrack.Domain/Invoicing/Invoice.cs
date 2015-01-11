using System;
using BLToolkit.DataAccess;
using BLToolkit.Mapping;

namespace LanceTrack.Domain.Invoicing
{
    [TableName("InvoiceData")]
    public class Invoice
    {
        [PrimaryKey]
        public string InvoiceNum { get; set; }

        public int ProjectId { get; set; }

        public int UserId { get; set; }

        public DateTimeOffset At { get; set; }

        public bool IsPaid { get; set; }

        [MapField("InvoiceSum")]
        public decimal Sum { get; set; }

        [MapField("InvoiceHours")]
        public decimal Hours { get; set; }
    }
}