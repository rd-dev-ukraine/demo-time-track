using System;
using LanceTrack.Domain.Projects;
using LanceTrack.Server.Cqrs.ProjectTime.Dependencies;
using LinqToDB;
using LinqToDB.Data;

namespace LanceTrack.Server.Cqrs.DataAccess.ProjectTime
{
    public class DatabaseDailyTimeStorage : IDailyTimeStorage
    {
        public DatabaseDailyTimeStorage(DataConnection dbManager)
        {
            if (dbManager == null)
                throw new ArgumentNullException("dbManager");

            DbManager = dbManager;
        }

        private DataConnection DbManager { get; set; }

        public void SaveProjectDailyTime(DailyTime readModel)
        {
            DbManager.InsertOrReplace(readModel);
        }
    }
}