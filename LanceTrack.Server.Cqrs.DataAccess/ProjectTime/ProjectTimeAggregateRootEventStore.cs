using System;
using System.Collections.Generic;
using BLToolkit.Data;
using BLToolkit.Data.Linq;
using LanceTrack.Cqrs.Contract;
using LanceTrack.Server.Cqrs.ProjectTime;
using LanceTrack.Server.Cqrs.ProjectTime.Events;

namespace LanceTrack.Server.Cqrs.DataAccess.ProjectTime
{
    public class ProjectTimeAggregateRootEventStore : IEventStore<ProjectTimeAggregateRoot, int>, 
        IEventStoreAppendMethod<ProjectTimeTrackedEvent, ProjectTimeAggregateRoot,int>
    {
        public ProjectTimeAggregateRootEventStore(DbManager dbManager)
        {
            if (dbManager == null)
                throw new ArgumentNullException("dbManager");

            DbManager = dbManager;
        }

        private DbManager DbManager { get; set; }

        public IEnumerable<IEvent<ProjectTimeAggregateRoot, int>> ReadAggregateRootEvents(int aggregateRootId)
        {
            return DbManager.GetTable<ProjectTimeTrackedEvent>();
        }

        public void Append(ProjectTimeTrackedEvent @event)
        {
            DbManager.InsertWithIdentity(@event);
        }
    }
}