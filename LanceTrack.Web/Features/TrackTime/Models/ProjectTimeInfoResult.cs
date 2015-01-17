using System;
using System.Collections.Generic;
using LanceTrack.Domain.Infrastructure;
using LanceTrack.Domain.Projects;
using LanceTrack.Domain.UserAccounts;
using Newtonsoft.Json;
using TypeLite;

namespace LanceTrack.Web.Features.TrackTime.Models
{
    [TsClass(Module = "Api")]
    public class ProjectTimeInfoResult
    {
        public int CurrentUserId { get; set; }

        [JsonConverter(typeof(DateConverter))]
        public DateTime StartDate { get; set; }

        [JsonConverter(typeof(DateConverter))]
        public DateTime EndDate { get; set; }

        public List<Project> Projects { get; set; }

        public List<DailyTime> Time { get; set; }

        public List<UserAccount> Users { get; set; } 
    }
}