using System;
using System.Linq;
using BLToolkit.Data;
using BLToolkit.Data.Linq;
using LanceTrack.Server.Dependencies.Project;
using LanceTrack.Server.Dependencies.ProjectTime;
using LanceTrack.Server.Dependencies.TimeTracking.ReadModels.ProjectDailyTime;

namespace LanceTrack.DataAccess.ProjectTime
{
    public class DatabaseProjectTimeRepository : IProjectDailyTimeStorage, IProjectTimeRepository
    {
        public DatabaseProjectTimeRepository(DbManager dbManager)
        {
            if (dbManager == null)
                throw new ArgumentNullException("dbManager");

            DbManager = dbManager;
        }

        private DbManager DbManager { get; set; }

        public IQueryable<ProjectDailyTime> GetProjectDailyTime(int projectId, int userId, DateTime startDate, DateTime endDate)
        {
            startDate = startDate.Date;
            endDate = endDate.Date;

            return DbManager.GetTable<ProjectDailyTime>()
                .Where(t => t.ProjectId == projectId)
                .Where(t => startDate <= t.Date && t.Date <= endDate)
                .Where(t => t.UserId == userId);
        }

        public void SaveProjectDailyTime(ProjectDailyTime projectDailyTime)
        {
            DbManager.InsertOrReplace(projectDailyTime);
        }
    }
}