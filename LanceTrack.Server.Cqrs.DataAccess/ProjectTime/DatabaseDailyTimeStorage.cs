using System;
using BLToolkit.Data;
using BLToolkit.Data.Linq;
using LanceTrack.Domain.Projects;
using LanceTrack.Server.Cqrs.ProjectTime.Dependencies;

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

        public void SaveProjectDailyTime(ProjectDailyTime readModel)
        {
            DbManager.InsertOrReplace(readModel);
        }
    }
}