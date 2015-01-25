using System;
using System.Collections.Generic;
using System.Linq;
using LanceTrack.Cqrs.Contract;
using LanceTrack.Server.Cqrs.ProjectTime;
using LanceTrack.Server.Cqrs.ProjectTime.Events;
using LinqToDB;
using LinqToDB.Data;

namespace LanceTrack.Server.Cqrs.DataAccess.ProjectTime
{
    public class ProjectTimeAggregateRootEventStore : IEventStore<ProjectTimeAggregateRoot, int>,
        IEventStoreAppendMethod<TimeTrackedEvent, ProjectTimeAggregateRoot, int>,
        IEventStoreAppendMethod<InvoiceEvent, ProjectTimeAggregateRoot, int>
    {
        public ProjectTimeAggregateRootEventStore(DataConnection dbManager)
        {
            if (dbManager == null)
                throw new ArgumentNullException("dbManager");

            DbManager = dbManager;
        }

        private DataConnection DbManager { get; set; }

        public IEnumerable<IEvent<ProjectTimeAggregateRoot, int>> ReadAggregateRootEvents(int aggregateRootId)
        {
            return DbManager.GetTable<TimeTrackedEvent>()
                            .Where(e => e.ProjectId == aggregateRootId)
                            .ToArray()
                            .Cast<IEvent<ProjectTimeAggregateRoot, int>>()
            .Concat(
                   DbManager.GetTable<InvoiceEvent>()
                            .Where(e => e.ProjectId == aggregateRootId)
                            .ToArray())
            .OrderBy(e => e.Id)
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