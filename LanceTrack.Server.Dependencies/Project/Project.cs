using System;
using BLToolkit.DataAccess;

namespace LanceTrack.Server.Dependencies.Project
{
    [TableName("Project")]
    public class Project
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public ProjectStatus Status { get; set; }

        public DateTimeOffset StartDate { get; set; }

        public DateTimeOffset? EndDate { get; set; }

        public int? MaxTotalHoursPerDay { get; set; }

        public int? MaxTotalHours { get; set; }
    }
}