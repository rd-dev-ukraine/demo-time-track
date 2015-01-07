using System;
using LanceTrack.Cqrs.Contract;
using LanceTrack.Cqrs.Server;

namespace LanceTrack.Server.Cqrs.ProjectTime
{
    public class ProjectTimeAggregateRootServer : AggregateRootServer<ProjectTimeAggregateRoot, int>
    {
        public ProjectTimeAggregateRootServer(IEventStore<ProjectTimeAggregateRoot, int> eventStore, Func<ProjectTimeAggregateRoot> aggregateRootFactory) 
            : base(eventStore, aggregateRootFactory)
        {
        }
    }
}