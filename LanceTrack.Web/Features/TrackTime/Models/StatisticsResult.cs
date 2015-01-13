using System.Collections.Generic;
using LanceTrack.Domain.Projects;
using TypeLite;

namespace LanceTrack.Web.Features.TrackTime.Models
{
    [TsClass(Module = "Api")]
    public class StatisticsResult
    {
        public decimal TotalHours { get; set; }

        public decimal TotalEarnings { get; set; }

        public List<ProjectUserSummary> ProjectStatistics { get; set; }
    }
}