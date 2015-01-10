using System;
using System.Linq;
using BLToolkit.Data;
using LanceTrack.Domain.ProjectUserInfo;
using LanceTrack.Server.Dependencies.ProjectUserInfo;

namespace LanceTrack.DataAccess.ProjectUserSummary
{
    public class DatabaseProjectUserSummaryAccessor  : IProjectUserSummaryAccessor
    {
        public DatabaseProjectUserSummaryAccessor(DbManager dbManager)
        {
            if (dbManager == null)
                throw new ArgumentNullException("dbManager");

            DbManager = dbManager;
        }

        private DbManager DbManager { get; set; }
        
        public ProjectUserSummaryData ProjectUserSummary(int userId, int projectId)
        {
            return DbManager.GetTable<ProjectUserSummaryData>()
                            .SingleOrDefault(r => r.UserId == userId && r.ProjectId == projectId);
        }
    }
}