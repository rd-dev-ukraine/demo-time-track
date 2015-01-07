using System;
using BLToolkit.DataAccess;
using LanceTrack.Cqrs.Contract;

namespace LanceTrack.Server.Cqrs.ProjectTime.Events
{
    [TableName("TimeRegistrationEvents")]
    public class ProjectTimeTrackedEvent : IEvent<ProjectTimeAggregateRoot, int>
    {
        [PrimaryKey, Identity]
        public int Id { get; set; }

        public int UserId { get; set; }

        public int ProjectId { get; set; }

        public DateTimeOffset At { get; set; }

        public decimal Hours { get; set; }

        public int RegisteredByUserId { get; set; }

        public DateTimeOffset RegisteredAt { get; set; }

        int IEvent<ProjectTimeAggregateRoot, int>.AggregateRootId { get { return ProjectId; } }
    }
}