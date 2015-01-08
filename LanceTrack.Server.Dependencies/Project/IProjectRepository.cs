using System;
using System.Linq;

namespace LanceTrack.Server.Dependencies.Project
{
    public interface IProjectRepository
    {
        Project GetById(int id);

        IQueryable<Project> GetReportableProjectsForUser(int userId, DateTime startDate, DateTime endDate);

        ProjectUserData GetProjectPermissionsForUser(int userId, int projectId);
    }
}