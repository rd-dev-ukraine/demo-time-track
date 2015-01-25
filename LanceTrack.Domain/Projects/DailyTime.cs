using System;
using LanceTrack.Domain.Infrastructure;
using LinqToDB.Mapping;
using Newtonsoft.Json;
using TypeLite;

namespace LanceTrack.Domain.Projects
{
    [Table("DailyTimeData"), TsClass(Module = "Api")]
    public class DailyTime
    {
        [PrimaryKey]
        public int ProjectId { get;set; }

        [PrimaryKey]
        public int UserId { get; set; }

        [PrimaryKey]
        [JsonConverter(typeof(DateConverter))]
        public DateTime Date { get; set; }

        [Column]
        [JsonConverter(typeof(DecimalZeroToEmptyConverter))]
        public decimal TotalHours { get; set; }

        [Column]
        [JsonConverter(typeof(DecimalZeroToEmptyConverter))]
        public decimal BilledHours { get; set; }

        [Column]
        [JsonConverter(typeof(DecimalZeroToEmptyConverter))]
        public decimal PaidHours { get; set; }

        [Column]
        [JsonConverter(typeof(DecimalZeroToEmptyConverter))]
        public decimal HourlyRate { get; set; }
    }
}