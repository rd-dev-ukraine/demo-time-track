using System;
using LanceTrack.Cqrs.Server;

namespace LanceTrack.Server.Cqrs.ProjectTime
{
    public class ProjectTimeAggregateRootServer : AggregateRootServer<ProjectTimeAggregateRoot, int>
    {
        public ProjectTimeAggregateRootServer(ProjectTimeAggregateRootEventStore eventStore, Func<ProjectTimeAggregateRoot> aggregateRootFactory) 
            : base(eventStore, aggregateRootFactory)
        {
        }
    }
}