using System.Collections.Generic;
using LanceTrack.Domain.ProjectUserInfo;

namespace LanceTrack.Web.Features.TrackTime.Models
{
    public class StatisticsResult
    {
        public decimal TotalHours { get; set; }

        public decimal TotalEarnings { get; set; }

        public List<ProjectUserSummaryData> ProjectStatistics { get; set; } 
    }
}