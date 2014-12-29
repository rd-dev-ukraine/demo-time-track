using System;
using System.Collections.Generic;
using System.Linq;
using BLToolkit.Data;
using BLToolkit.Data.Linq;
using LanceTrack.Server.TimeTracking;
using LanceTrack.Server.TimeTracking.Events;

namespace LanceTrack.DataAccess.TimeTracking
{
    public class DatabaseTimeTrackingEventRepository: ITimeTrackingEventRepository
    {
        public DatabaseTimeTrackingEventRepository(DbManager dbManager)
        {
            if (dbManager == null)
                throw new ArgumentNullException("dbManager");

            DbManager = dbManager;
        }

        private DbManager DbManager { get; set; }

        public IEnumerable<TimeTrackedEvent> ReadTimeTrackedEvents(int projectId)
        {
            return DbManager.GetTable<TimeTrackedEvent>()
                            .Where(e => e.ProjectId == projectId);
        }

        public void StoreEvent(TimeTrackedEvent @event)
        {
            @event.Id = Convert.ToInt32(DbManager.InsertWithIdentity(@event));
        }
    }
}