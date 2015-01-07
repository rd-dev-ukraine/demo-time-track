using System;
using BLToolkit.Data;
using BLToolkit.Data.Linq;
using LanceTrack.Server.Cqrs.ProjectTime.Dependencies;
using LanceTrack.Server.Cqrs.ProjectTime.ReadModels;
using LanceTrack.Server.Dependencies.ProjectDailyTime;

namespace LanceTrack.Server.Cqrs.DataAccess.ProjectTime
{
    public class ProjectDailyTimeStorage : IProjectDailyTimeStorage
    {
        public ProjectDailyTimeStorage(DbManager dbManager)
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