using System;
using System.Collections.Generic;
using System.Linq;

namespace LanceTrack.Server.Dependencies.Project
{
    public interface IProjectRepository
    {
        Project GetById(int id);

        IEnumerable<Project> GetReportableProjectsForUser(int userId, DateTime startDate, DateTime endDate);

        ProjectUserData GetProjectPermissionsForUser(int userId, int projectId);

        IEnumerable<Project> GetProjects(int userId);
    }
}