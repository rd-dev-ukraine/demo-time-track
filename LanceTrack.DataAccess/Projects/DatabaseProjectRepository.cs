using System;
using System.Linq;
using BLToolkit.Data;
using LanceTrack.Server.Dependencies.Project;

namespace LanceTrack.DataAccess.Projects
{
    public class DatabaseProjectRepository : IProjectRepository
    {
        public DatabaseProjectRepository(DbManager dbManager)
        {
            if (dbManager == null)
                throw new ArgumentNullException("dbManager");

            DbManager = dbManager;
        }

        private DbManager DbManager { get; set; }

        public Project GetById(int id)
        {
            return DbManager.GetTable<Project>().SingleOrDefault(p => p.Id == id);
        }

        public Server.Dependencies.Project.ProjectPermissions GetProjectPermissionsForUser(int userId, int projectId)
        {
            var perms = DbManager.GetTable<ProjectPermissions>().SingleOrDefault(pp => pp.UserId == userId && pp.ProjectId == projectId);
            if (perms == null)
                return Server.Dependencies.Project.ProjectPermissions.None;

            return perms.UserPermissions;
        }

        public IQueryable<Project> GetReportableProjectsForUser(int userId, DateTime startDate, DateTime endDate)
        {
            startDate = startDate.Date;
            endDate = endDate.Date;

            var projectPermissions = DbManager.GetTable<ProjectPermissions>();
            return DbManager.GetTable<Project>()
                .Where(p => projectPermissions.Any(perm => perm.ProjectId == p.Id && perm.UserId == userId))
                .Where(p => p.Status == ProjectStatus.Active)
                .Where(p => p.StartTime >= startDate && p.StartTime <= endDate);
        }
    }
}