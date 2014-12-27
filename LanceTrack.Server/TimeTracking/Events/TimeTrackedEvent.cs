using System;
using BLToolkit.DataAccess;

namespace LanceTrack.Server.TimeTracking.Events
{
    [TableName("TimeRegistrationEvents")]
    public class TimeTrackedEvent
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