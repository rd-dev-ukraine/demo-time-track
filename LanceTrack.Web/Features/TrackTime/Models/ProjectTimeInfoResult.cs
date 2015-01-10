using System;
using System.Collections.Generic;
using LanceTrack.Domain.Infrastructure;
using LanceTrack.Domain.ProjectTime;
using Newtonsoft.Json;

namespace LanceTrack.Web.Features.TrackTime.Models
{
    public class ProjectTimeInfoResult
    {
        [JsonConverter(typeof(DateConverter))]
        public DateTime StartDate { get; set; }

        [JsonConverter(typeof(DateConverter))]
        public DateTime EndDate { get; set; }

        public List<ProjectTimeInfo> Projects { get; set; } 
    }
}