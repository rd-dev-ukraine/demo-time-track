using System.Collections.Generic;

namespace LanceTrack.Domain.ProjectTime
{
    public class ProjectTimeInfo
    {
        public int ProjectId { get; set; }

        public string ProjectTitle { get; set; }

        public List<TimeRecord> Time { get; set; } 
    }
}