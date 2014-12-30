namespace LanceTrack.Server.Projects
{
    public interface IProjectPermissionsService
    {
        ProjectPermissions CalculatePermissions(int userId, int projectId);
    }
}