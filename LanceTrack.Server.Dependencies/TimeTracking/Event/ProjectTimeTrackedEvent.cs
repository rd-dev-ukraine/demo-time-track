using System;
using BLToolkit.DataAccess;

namespace LanceTrack.Server.Dependencies.TimeTracking.Event
{
    [TableName("TimeRegistrationEvents")]
    public class ProjectTimeTrackedEvent
    {
        [PrimaryKey, Identity]
        public int Id { get; set; }

        public int UserId { get; set; }

        public int ProjectId { get; set; }

        public DateTimeOffset At { get; set; }

        public decimal Hours { get; set; }

        public int RegisteredByUserId { get; set; }

        public DateTimeOffset RegisteredAt { get; set; }
    }
}