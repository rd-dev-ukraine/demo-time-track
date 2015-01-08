namespace LanceTrack.Server.Dependencies.Project
{
    public interface IProjectService
    {
        Project GetById(int id);

        ProjectUserData GetProjectUserData(int userId, int projectId);
    }
}