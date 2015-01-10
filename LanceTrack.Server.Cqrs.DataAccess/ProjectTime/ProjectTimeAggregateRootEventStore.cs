using System;
using System.Collections.Generic;
using System.Linq;
using BLToolkit.Data;
using BLToolkit.Data.Linq;
using BLToolkit.DataAccess;
using LanceTrack.Cqrs.Contract;
using LanceTrack.Server.Cqrs.ProjectTime;
using LanceTrack.Server.Cqrs.ProjectTime.Events;

namespace LanceTrack.Server.Cqrs.DataAccess.ProjectTime
{
    public class ProjectTimeAggregateRootEventStore : DataAccessor, IEventStore<ProjectTimeAggregateRoot, int>,
        IEventStoreAppendMethod<ProjectTimeTrackedEvent, ProjectTimeAggregateRoot, int>
    {
        public ProjectTimeAggregateRootEventStore(DbManager dbManager)
            : base(dbManager)
        {
        }

        public IEnumerable<IEvent<ProjectTimeAggregateRoot, int>> ReadAggregateRootEvents(int aggregateRootId)
        {
            return DbManager.GetTable<ProjectTimeTrackedEvent>()
                            .Where(e => e.ProjectId == aggregateRootId)
                            .OrderBy(e => e.At)
                            .ToArray();
        }

        public void Append(ProjectTimeTrackedEvent @event)
        {
            DbManager.InsertWithIdentity(@event);
        }
    }
}