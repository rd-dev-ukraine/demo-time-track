using System;
using System.Linq;

namespace LanceTrack.Server.Dependencies.Project
{
    public interface IProjectRepository
    {
        Project GetById(int id);

        IQueryable<Project> GetReportableProjectsForUser(int userId, DateTime startDate, DateTime endDate);

        ProjectPermissions GetProjectPermissionsForUser(int userId, int projectId);
    }
}