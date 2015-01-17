using System;
using BLToolkit.DataAccess;
using LanceTrack.Domain.Infrastructure;
using Newtonsoft.Json;
using TypeLite;

namespace LanceTrack.Domain.Projects
{
    [TableName("DailyTimeData"), TsClass(Module = "Api")]
    public class DailyTime
    {
        [PrimaryKey]
        public int ProjectId { get;set; }

        [PrimaryKey]
        public int UserId { get; set; }

        [PrimaryKey]
        [JsonConverter(typeof(DateConverter))]
        public DateTime Date { get; set; }

        [JsonConverter(typeof(DecimalZeroToEmptyConverter))]
        public decimal TotalHours { get; set; }

        [JsonConverter(typeof(DecimalZeroToEmptyConverter))]
        public decimal BilledHours { get; set; }

        [JsonConverter(typeof(DecimalZeroToEmptyConverter))]
        public decimal PaidHours { get; set; }

        [JsonConverter(typeof(DecimalZeroToEmptyConverter))]
        public decimal HourlyRate { get; set; }
    }
}