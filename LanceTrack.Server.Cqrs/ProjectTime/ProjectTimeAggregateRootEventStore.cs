using System;
using System.Collections.Generic;
using System.Linq;
using LanceTrack.Cqrs.Contract;


namespace LanceTrack.Server.Cqrs.ProjectTime
{
    public class ProjectTimeAggregateRootEventStore : IEventStore<ProjectTimeAggregateRoot, int>, 
        IEventStoreAppendMethod<ProjectTimeTrackedEvent, ProjectTimeAggregateRoot,int>
    {
        private readonly IProjectTimeAggregateRootEventAccessor _eventAccessor;

        public ProjectTimeAggregateRootEventStore(IProjectTimeAggregateRootEventAccessor eventAccessor)
        {
            if (eventAccessor == null)
                throw new ArgumentNullException("eventAccessor");

            _eventAccessor = eventAccessor;
        }

        public IEnumerable<IEvent<ProjectTimeAggregateRoot, int>> ReadAggregateRootEvents(int aggregateRootId)
        {
            return _eventAccessor.ReadProjectTimeTrackedEvents(aggregateRootId);
        }

        public void Append(ProjectTimeTrackedEvent @event)
        {
            _eventAccessor.Append(@event);
        }
    }
}