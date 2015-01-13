using System;
using System.Collections.Generic;
using LanceTrack.Domain.Infrastructure;
using LanceTrack.Domain.Projects;
using LanceTrack.Domain.UserAccounts;
using Newtonsoft.Json;

namespace LanceTrack.Web.Features.TrackTime.Models
{
    public class ProjectTimeInfoResult
    {
        [JsonConverter(typeof(DateConverter))]
        public DateTime StartDate { get; set; }

        [JsonConverter(typeof(DateConverter))]
        public DateTime EndDate { get; set; }

        public List<Project> Projects { get; set; }

        public List<ProjectDailyTime> Time { get; set; }

        public List<UserAccount> Users { get; set; } 
    }
}