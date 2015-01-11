using System;
using BLToolkit.DataAccess;
using BLToolkit.Mapping;
using LanceTrack.Cqrs.Contract;

namespace LanceTrack.Server.Cqrs.ProjectTime.Events
{
    [TableName("InvoiceEvents")]
    public class InvoiceEvent : IEvent<ProjectTimeAggregateRoot, int>
    {
        [Identity, PrimaryKey]
        public int Id { get; set; }

        public int ProjectId { get; set; }

        public int UserId { get; set; }

        public DateTimeOffset At { get; set; }

        public InvoiceEventType EventType { get; set; }

        public string InvoiceNum { get; set; }

        public decimal InvoiceSum { get; set; }

        public decimal Hours { get; set; }

        public int RegisteredByUserId { get; set; }

        public DateTimeOffset RegisteredAt { get; set; }

        [MapIgnore]
        public int AggregateRootId { get { return ProjectId; } }
    }
}