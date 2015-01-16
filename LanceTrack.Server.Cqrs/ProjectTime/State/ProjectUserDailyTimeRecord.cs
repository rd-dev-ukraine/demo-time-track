using System;

namespace LanceTrack.Server.Cqrs.ProjectTime.State
{
    public class ProjectUserDailyTimeRecord
    {
        public DateTime At { get; set; }
        public decimal BilledHours { get; set; }
        public decimal HourlyRate { get; set; }
        public DateTimeOffset HourlyRateDate { get; set; }
        public decimal Hours { get; set; }
        public int ProjectId { get; set; }
        public int UserId { get; set; }
    }
}