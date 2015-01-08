using System;
using System.Collections.Generic;
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

        public ProjectUserData GetProjectPermissionsForUser(int userId, int projectId)
        {
            return DbManager.GetTable<ProjectUserData>()
                            .SingleOrDefault(pp => pp.UserId == userId && pp.ProjectId == projectId);
        }

        public IEnumerable<Project> GetProjects(int userId)
        {
            return GetActiveProjects(userId);
        }

        public IQueryable<Project> GetReportableProjectsForUser(int userId, DateTime startDate, DateTime endDate)
        {
            startDate = startDate.Date;
            endDate = endDate.Date;

            return GetActiveProjects(userId).Where(p => p.StartDate <= endDate && (p.EndDate == null || p.EndDate >= startDate));
        }

        private IQueryable<Project> GetActiveProjects(int userId)
        {
            var projectPermissions = DbManager.GetTable<ProjectUserData>();
            return DbManager.GetTable<Project>()
                            .Where(p => projectPermissions.Any(perm => perm.ProjectId == p.Id && perm.UserId == userId))
                            .Where(p => p.Status == ProjectStatus.Active);
        }
    }
}