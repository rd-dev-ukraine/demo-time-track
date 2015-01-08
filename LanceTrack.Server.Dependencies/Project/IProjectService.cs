using System.Collections.Generic;

namespace LanceTrack.Server.Dependencies.Project
{
    public interface IProjectService
    {
        IEnumerable<Project> GetProjects();

        Project GetById(int id);

        ProjectUserData GetProjectUserData(int userId, int projectId);
    }
}