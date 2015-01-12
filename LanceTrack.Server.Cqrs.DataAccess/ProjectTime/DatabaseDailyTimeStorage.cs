using System;
using BLToolkit.Data;
using BLToolkit.Data.Linq;
using LanceTrack.Server.Cqrs.ProjectTime.Dependencies;
using LanceTrack.Server.Dependencies.ProjectDailyTime;

namespace LanceTrack.Server.Cqrs.DataAccess.ProjectTime
{
    public class DatabaseDailyTimeStorage : IDailyTimeStorage
    {
        public DatabaseDailyTimeStorage(DbManager dbManager)
        {
            if (dbManager == null)
                throw new ArgumentNullException("dbManager");

            DbManager = dbManager;
        }

        private DbManager DbManager { get; set; }

        public void SaveProjectDailyTime(ProjectDailyTimeData readModel)
        {
            DbManager.InsertOrReplace(readModel);
        }
    }
}