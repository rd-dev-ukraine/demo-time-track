using System;
using LanceTrack.Cqrs.Contract;
using LinqToDB.Mapping;

namespace LanceTrack.Server.Cqrs.ProjectTime.Events
{
    [Table("InvoiceEvents")]
    public class InvoiceEvent : IEvent<ProjectTimeAggregateRoot, int>
    {
        [Identity, PrimaryKey]
        public int Id { get; set; }

        [Column]
        public int ProjectId { get; set; }

        [Column]
        public int UserId { get; set; }

        [Column]
        public DateTimeOffset At { get; set; }

        [Column]
        public InvoiceEventType EventType { get; set; }

        [Column]
        public string InvoiceNum { get; set; }

        [Column]
        public decimal InvoiceSum { get; set; }

        [Column]
        public decimal Hours { get; set; }

        [Column]
        public int RegisteredByUserId { get; set; }

        [Column]
        public DateTimeOffset RegisteredAt { get; set; }

        [NotColumn]
        public int AggregateRootId { get { return ProjectId; } }
    }
}