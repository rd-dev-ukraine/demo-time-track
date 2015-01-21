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

        public IQueryable<DailyTime> GetProjectDailyTime(DateTime startDate, DateTime endDate)
        {
            startDate = startDate.Date;
            endDate = endDate.Date;

            return DbManager.GetTable<DailyTime>()
                            .Where(t => startDate <= t.Date && t.Date <= endDate);
        }

        public IQueryable<ProjectUserInfo> GetProjectUserInfo()
        {
            return DbManager.GetTable<ProjectUserInfo>();
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
            return DbManager.GetTable<Project>()
                            .Join(ProjectUserInfo().Where(i => i.UserId == userId),
                                  p => p.Id,
                                  i => i.ProjectId,
                                  (p, i) => new Project
                                    {
                                        EndDate = p.EndDate,
                                        Id = p.Id,
                                        MaxTotalHours = p.MaxTotalHours,
                                        Name = p.Name,
                                        Permissions = i.UserPermissions,
                                        StartDate = p.StartDate,
                                        Status = p.Status
                                    })
                            .Where(p => (p.Permissions & permissions) != ProjectPermissions.None)
                            .Where(p => p.Status == ProjectStatus.Active);
        }

        private IQueryable<ProjectUserInfo> ProjectUserInfo()
        {
            return DbManager.GetTable<ProjectUserInfo>();
        }
    }
}