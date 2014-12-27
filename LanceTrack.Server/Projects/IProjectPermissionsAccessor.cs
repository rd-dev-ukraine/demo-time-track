namespace LanceTrack.Server.Projects
{
    public interface IProjectPermissionsAccessor
    {
        ProjectPermissions GetProjectPermissionsForUser(int userId, int projectId);
    }
}