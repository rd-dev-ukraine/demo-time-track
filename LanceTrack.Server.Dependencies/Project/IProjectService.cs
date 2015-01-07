namespace LanceTrack.Server.Dependencies.Project
{
    public interface IProjectService
    {
        Project GetById(int id);

        ProjectPermissions CalculatePermissions(int userId, int projectId);
    }
}