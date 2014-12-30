using System;
using LanceTrack.Domain.Projects;

namespace LanceTrack.Domain.ProjectTime
{
    public class Project
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public ProjectStatus Status { get; set; }

        public DateTimeOffset StartTime { get; set; }

        public DateTimeOffset? EndTime { get; set; }

        public int? MaxTotalHoursPerDay { get; set; }

        public int? MaxTotalHours { get; set; }
    }
}