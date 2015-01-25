using System;
using LanceTrack.Domain.Infrastructure;
using LinqToDB.Mapping;
using Newtonsoft.Json;
using TypeLite;

namespace LanceTrack.Domain.Invoicing
{
    [Table("InvoiceData"), TsClass(Module = "Api")]
    public class Invoice
    {
        [PrimaryKey]
        public string InvoiceNum { get; set; }

        [Column]
        public int ProjectId { get; set; }

        [Column]
        public DateTimeOffset At { get; set; }

        [Column]
        public bool IsPaid { get; set; }

        [Column]
        public bool IsCancelled { get; set; }

        [Column("InvoiceTotalSum"), JsonConverter(typeof(DecimalZeroToEmptyConverter))]
        public decimal Sum { get; set; }

        [Column("InvoiceTotalHours"), JsonConverter(typeof(DecimalZeroToEmptyConverter))]
        public decimal Hours { get; set; }

        [Column]
        [JsonConverter(typeof(DecimalZeroToEmptyConverter))]
        public decimal? ReceivedSum { get; set; }

        [Column]
        public int BilledByUserId { get; set; }
    }
}