using System;
using LanceTrack.Domain.Infrastructure;
using Newtonsoft.Json;

namespace LanceTrack.Domain.ProjectTime
{
    public class TimeRecord
    {
        public decimal Hours { get; set; }

        [JsonConverter(typeof(DateConverter))]
        public DateTime Date { get; set; }
    }
}