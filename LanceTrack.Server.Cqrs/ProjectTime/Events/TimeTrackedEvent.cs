using System;
using LanceTrack.Cqrs.Contract;
using LinqToDB.Mapping;

namespace LanceTrack.Server.Cqrs.ProjectTime.Events
{
    [Table("TimeRegistrationEvents")]
    public class TimeTrackedEvent : IEvent<ProjectTimeAggregateRoot, int>
    {
        [PrimaryKey, Identity]
        public int Id { get; set; }

        [Column]
        public int UserId { get; set; }

        [Column]
        public int ProjectId { get; set; }

        [Column]
        public DateTime At { get; set; }

        [Column]
        public decimal Hours { get; set; }

        [Column]
        public decimal HourlyRate { get; set; }

        [Column]
        public int RegisteredByUserId { get; set; }

        [Column]
        public DateTimeOffset RegisteredAt { get; set; }

        [NotColumn]
        int IEvent<ProjectTimeAggregateRoot, int>.AggregateRootId { get { return ProjectId; } }
    }
}