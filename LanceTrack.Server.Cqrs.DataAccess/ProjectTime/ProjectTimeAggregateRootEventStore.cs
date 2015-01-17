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
        IEventStoreAppendMethod<TimeTrackedEvent, ProjectTimeAggregateRoot, int>,
        IEventStoreAppendMethod<InvoiceEvent, ProjectTimeAggregateRoot, int>
    {
        public ProjectTimeAggregateRootEventStore(DbManager dbManager)
            : base(dbManager)
        {
        }

        public IEnumerable<IEvent<ProjectTimeAggregateRoot, int>> ReadAggregateRootEvents(int aggregateRootId)
        {
            return DbManager.GetTable<TimeTrackedEvent>()
                            .Where(e => e.ProjectId == aggregateRootId)
                            .ToArray()
                            .Cast<IEvent<ProjectTimeAggregateRoot, int>>()
            .Concat(
                   DbManager.GetTable<InvoiceEvent>()
                            .Where(e => e.ProjectId == aggregateRootId)
                            .OrderBy(e => e.At)
                            .ToArray())
            .OrderBy(e => e.RegisteredAt)
            .ToArray();
        }

        public void Append(TimeTrackedEvent @event)
        {
            DbManager.InsertWithIdentity(@event);
        }

        public void Append(InvoiceEvent @event)
        {
            DbManager.InsertWithIdentity(@event);
        }
    }
}