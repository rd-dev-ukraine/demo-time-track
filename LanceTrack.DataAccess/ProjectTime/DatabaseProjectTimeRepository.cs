using System;
using System.Linq;
using BLToolkit.Data;
using BLToolkit.Data.Linq;
using LanceTrack.Domain.ProjectTime;
using LanceTrack.Server.Dependencies.TimeTracking.ReadModels;

namespace LanceTrack.DataAccess.ProjectTime
{
    public class DatabaseProjectTimeRepository : IProjectDailyTimeStorage
    {
        public DatabaseProjectTimeRepository(DbManager dbManager)
        {
            if (dbManager == null)
                throw new ArgumentNullException("dbManager");

            DbManager = dbManager;
        }

        private DbManager DbManager { get; set; }

        public IQueryable<ProjectDailyTime> GetProjectDailyTime(int projectId, DateTime startDate, DateTime endDate, int? userId = null)
        {
            startDate = startDate.Date;
            endDate = endDate.Date;

            return DbManager.GetTable<ProjectDailyTime>()
                .Where(t => t.ProjectId == projectId)
                .Where(t => startDate <= t.Date && t.Date <= endDate)
                .Where(t => userId == null || t.UserId == userId);
        }

        public void SaveProjectDailyTime(ProjectDailyTime projectDailyTime)
        {
            DbManager.InsertOrReplace(projectDailyTime);
        }
    }
}