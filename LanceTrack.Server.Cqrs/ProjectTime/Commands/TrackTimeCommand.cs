using System;
using LanceTrack.Cqrs.Contract;

namespace LanceTrack.Server.Cqrs.ProjectTime.Commands
{
    public class TrackTimeCommand : ICommand<ProjectTimeAggregateRoot, int>
    {
        public int ProjectId { get; set; }

        public int ForUserId { get; set; }

        public int ByUserId { get; set; }

        public DateTimeOffset At { get; set; }

        public decimal Hours { get; set; }

        int ICommand<ProjectTimeAggregateRoot, int>.AggregateRootId { get { return ProjectId; } }
    }
}