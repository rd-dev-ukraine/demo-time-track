using System;
using System.Linq;
using BLToolkit.Data;
using LanceTrack.Domain.Projects;
using LanceTrack.Server.Dependencies.Projects;

namespace LanceTrack.Server.DataAccess.Projects
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

        public IQueryable<Project> BillableProjects(int userId)
        {
            return Projects(userId, ProjectPermissions.BillProject);
        }

        public Project GetById(int id)
        {
            return DbManager.GetTable<Project>().SingleOrDefault(p => p.Id == id);
        }

        public IQueryable<ProjectDailyTime> GetProjectDailyTime(int userId, DateTime startDate, DateTime endDate)
        {
            startDate = startDate.Date;
            endDate = endDate.Date;

            return DbManager.GetTable<ProjectDailyTime>()
                .Join(ReportableProjects(userId), t => t.ProjectId, p => p.Id, (t, p) => t)
                .Where(t => startDate <= t.Date && t.Date <= endDate);
        }

        public ProjectUserData GetProjectPermissionsForUser(int userId, int projectId)
        {
            return DbManager.GetTable<ProjectUserData>()
                .SingleOrDefault(pp => pp.UserId == userId && pp.ProjectId == projectId);
        }

        public IQueryable<ProjectUserSummary> ProjectUserSummary(int userId)
        {
            return DbManager.GetTable<ProjectUserSummary>()
                .Where(a => a.UserId == userId)
                .Join(Projects(userId, ProjectPermissions.TrackSelf), a => a.ProjectId, p => p.Id, (a, p) => a);
        }

        public IQueryable<Project> ReportableProjects(int userId)
        {
            return Projects(userId, ProjectPermissions.TrackSelf);
        }

        private IQueryable<Project> Projects(int userId, ProjectPermissions permissions)
        {
            var pud = ProjectUserData();
            return DbManager.GetTable<Project>()
                .Where(p => pud.Any(perm => perm.ProjectId == p.Id &&
                                            perm.UserId == userId &&
                                            (perm.UserPermissions & permissions) != ProjectPermissions.None))
                .Where(p => p.Status == ProjectStatus.Active);
        }

        private IQueryable<ProjectUserData> ProjectUserData()
        {
            return DbManager.GetTable<ProjectUserData>();
        }
    }
}