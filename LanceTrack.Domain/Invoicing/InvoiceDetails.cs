using BLToolkit.DataAccess;
using LanceTrack.Domain.Infrastructure;
using Newtonsoft.Json;
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

        [JsonConverter(typeof(DecimalZeroToEmptyConverter))]
        public decimal UserSum { get; set; }

        [JsonConverter(typeof(DecimalZeroToEmptyConverter))]
        public decimal UserHours { get; set; }

        [JsonConverter(typeof(DecimalZeroToEmptyConverter))]
        public decimal? UserReceivedSum { get; set; }
    }
}