using System;
using System.Collections.Generic;
using System.Linq;
using BLToolkit.Data;
using BLToolkit.Data.Linq;
using LanceTrack.Server.Dependencies.Cqrs.ProjectTime;
using LanceTrack.Server.Dependencies.TimeTracking.Event;


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

        public IEnumerable<ProjectTimeTrackedEvent> ReadTimeTrackedEvents(int projectId)
        {
            return DbManager.GetTable<ProjectTimeTrackedEvent>()
                            .Where(e => e.ProjectId == projectId);
        }

        public void StoreEvent(ProjectTimeTrackedEvent @event)
        {
            @event.Id = Convert.ToInt32(DbManager.InsertWithIdentity(@event));
        }

        public IEnumerable<IProjectEvent> All()
        {
            return DbManager.GetTable<ProjectTimeTrackedEvent>();
        }
    }
}