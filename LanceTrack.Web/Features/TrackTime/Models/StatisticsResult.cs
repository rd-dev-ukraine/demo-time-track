using System.Collections.Generic;
using LanceTrack.Domain.Projects;

namespace LanceTrack.Web.Features.TrackTime.Models
{
    public class StatisticsResult
    {
        public decimal TotalHours { get; set; }

        public decimal TotalEarnings { get; set; }

        public List<ProjectUserSummary> ProjectStatistics { get; set; } 
    }
}