using System;
using System.Linq;
using System.Reflection;
using LanceTrack.Domain.Projects;
using LanceTrack.Server.Dependencies.Projects;
using LinqToDB;
using LinqToDB.Data;

namespace LanceTrack.Server.DataAccess.Projects
{
    public class DatabaseProjectRepository : IProjectRepository
    {
        public DatabaseProjectRepository(DataConnection dataConnection)
        {
            Db = dataConnection;
        }

        private DataConnection Db { get; set; }


        public IQueryable<Project> BillableProjects(int userId)
        {
            return Projects(userId, ProjectPermissions.BillProject);
        }

        public ProjectBase GetById(int id)
        {
            return Db.GetTable<ProjectBase>().SingleOrDefault(p => p.Id == id);
        }

        public IQueryable<DailyTime> GetDailyTime(DateTime startDate, DateTime endDate)
        {
            startDate = startDate.Date;
            endDate = endDate.Date;

            return Db.GetTable<DailyTime>()
                            .Where(t => startDate <= t.Date && t.Date <= endDate);
        }

        public IQueryable<ProjectUserInfo> GetProjectUserInfo()
        {
            return Db.GetTable<ProjectUserInfo>();
        }

        public IQueryable<ProjectUserSummary> ProjectUserSummary(int userId)
        {
            return Db.GetTable<ProjectUserSummary>()
                .Where(a => a.UserId == userId)
                .Join(Projects(userId, ProjectPermissions.TrackSelf), a => a.ProjectId, p => p.Id, (a, p) => a);
        }

        public IQueryable<Project> ReportableProjects(int userId)
        {
            return Projects(userId, ProjectPermissions.TrackSelf);
        }

        private IQueryable<Project> Projects(int userId, ProjectPermissions permissions)
        {
            return Db.GetTable<Project>()
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
            return Db.GetTable<ProjectUserInfo>();
        }
    }
}