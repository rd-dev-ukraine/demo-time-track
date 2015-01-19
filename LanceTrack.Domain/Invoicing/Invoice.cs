using System;
using BLToolkit.DataAccess;
using BLToolkit.Mapping;
using LanceTrack.Domain.Infrastructure;
using Newtonsoft.Json;
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

        [MapField("InvoiceTotalSum"), JsonConverter(typeof(DecimalZeroToEmptyConverter))]
        public decimal Sum { get; set; }

        [MapField("InvoiceTotalHours"), JsonConverter(typeof(DecimalZeroToEmptyConverter))]
        public decimal Hours { get; set; }

        [JsonConverter(typeof(DecimalZeroToEmptyConverter))]
        public decimal? ReceivedSum { get; set; }

        public int BilledByUserId { get; set; }
    }
}