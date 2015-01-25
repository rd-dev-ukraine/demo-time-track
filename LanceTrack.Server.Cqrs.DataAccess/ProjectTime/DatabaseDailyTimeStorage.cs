using System;
using LanceTrack.Domain.Projects;
using LanceTrack.Server.Cqrs.ProjectTime.Dependencies;
using LinqToDB;
using LinqToDB.Data;

namespace LanceTrack.Server.Cqrs.DataAccess.ProjectTime
{
    public class DatabaseDailyTimeStorage : IDailyTimeStorage
    {
        public DatabaseDailyTimeStorage(DataConnection db)
        {
            if (db == null)
                throw new ArgumentNullException("db");

            Db = db;
        }

        private DataConnection Db { get; set; }

        public void SaveProjectDailyTime(DailyTime readModel)
        {
            Db.InsertOrReplace(readModel);
        }
    }
}