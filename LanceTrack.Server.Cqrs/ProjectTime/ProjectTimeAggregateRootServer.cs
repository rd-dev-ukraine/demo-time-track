using System;
using LanceTrack.Cqrs.Contract;
using LanceTrack.Cqrs.Server;
using LanceTrack.Server.Cqrs.ProjectTime.State;

namespace LanceTrack.Server.Cqrs.ProjectTime
{
    public class ProjectTimeAggregateRootServer : AggregateRootServer<ProjectTimeAggregateRoot, ProjectTimeAggregateRootState, int>
    {
        public ProjectTimeAggregateRootServer(IEventStore<ProjectTimeAggregateRoot, int> eventStore, Func<ProjectTimeAggregateRoot> aggregateRootFactory) 
            : base(eventStore, aggregateRootFactory)
        {
        }
    }
}