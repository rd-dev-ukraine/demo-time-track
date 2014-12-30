using LanceTrack.Server.Dependencies.Project;

namespace LanceTrack.Server.Projects.Contract
{
    public interface IProjectPermissionsService
    {
        ProjectPermissions CalculatePermissions(int userId, int projectId);
    }
}