using LanceTrack.Domain.Infrastructure;
using LinqToDB.Mapping;
using Newtonsoft.Json;
using TypeLite;

namespace LanceTrack.Domain.Invoicing
{
    [Table("InvoiceDetailsData"), TsClass(Module = "Api")]
    public class InvoiceDetails
    {
        [PrimaryKey]
        public string InvoiceNum { get; set; }

        [PrimaryKey]
        public int UserId { get; set; }

        [Column]
        [JsonConverter(typeof(DecimalZeroToEmptyConverter))]
        public decimal UserSum { get; set; }

        [Column]
        [JsonConverter(typeof(DecimalZeroToEmptyConverter))]
        public decimal UserHours { get; set; }

        [Column]
        [JsonConverter(typeof(DecimalZeroToEmptyConverter))]
        public decimal? UserReceivedSum { get; set; }
    }
}