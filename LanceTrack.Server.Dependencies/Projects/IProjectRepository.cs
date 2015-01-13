using System;
using System.Linq;
using LanceTrack.Domain.Projects;

namespace LanceTrack.Server.Dependencies.Projects
{
    public interface IProjectRepository
    {
        IQueryable<Project> BillableProjects(int userId);
        Project GetById(int id);
        IQueryable<ProjectDailyTime> GetProjectDailyTime(int userId, DateTime startDate, DateTime endDate);
        ProjectUserData GetProjectPermissionsForUser(int userId, int projectId);
        IQueryable<ProjectUserSummary> ProjectUserSummary(int userId);
        IQueryable<Project> ReportableProjects(int userId);
    }
}