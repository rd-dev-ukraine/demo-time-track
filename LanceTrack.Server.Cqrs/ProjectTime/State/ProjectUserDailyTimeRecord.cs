using System;

namespace LanceTrack.Server.Cqrs.ProjectTime.State
{
    public class ProjectUserDailyTimeRecord
    {
        public int UserId { get; set; }

        public int ProjectId { get; set; }

        public DateTime At { get; set; }

        public decimal Hours { get; set; }

        public decimal HourlyRate { get; set; }

        public DateTimeOffset HourlyRateDate { get; set; }
    }
}