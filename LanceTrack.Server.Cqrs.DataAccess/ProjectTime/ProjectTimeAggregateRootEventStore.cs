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
        public ProjectTimeAggregateRootEventStore(DataConnection db)
        {
            if (db == null)
                throw new ArgumentNullException("db");

            Db = db;
        }

        private DataConnection Db { get; set; }

        public IEnumerable<IEvent<ProjectTimeAggregateRoot, int>> ReadAggregateRootEvents(int aggregateRootId)
        {
            return Db.GetTable<TimeTrackedEvent>()
                            .Where(e => e.ProjectId == aggregateRootId)
                            .ToArray()
                            .Cast<IEvent<ProjectTimeAggregateRoot, int>>()
            .Concat(
                   Db.GetTable<InvoiceEvent>()
                            .Where(e => e.ProjectId == aggregateRootId)
                            .ToArray())
            .OrderBy(e => e.Id)
            .ToArray();
        }

        public void Append(TimeTrackedEvent @event)
        {
            Db.InsertWithIdentity(@event);
        }

        public void Append(InvoiceEvent @event)
        {
            Db.InsertWithIdentity(@event);
        }
    }
}