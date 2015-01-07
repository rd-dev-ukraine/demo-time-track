using System;
using System.Linq;
using BLToolkit.Data;
using BLToolkit.Data.Linq;
using LanceTrack.Server.Dependencies.ProjectDailyTime;

namespace LanceTrack.DataAccess.ProjectTime
{
    public class DatabaseProjectTimeRepository : IProjectTimeRepository
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
    }
}