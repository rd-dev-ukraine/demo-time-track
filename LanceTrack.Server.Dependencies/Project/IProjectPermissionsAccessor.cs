namespace LanceTrack.Server.Dependencies.Project
{
    public interface IProjectPermissionsAccessor
    {
        ProjectPermissions GetProjectPermissionsForUser(int userId, int projectId);
    }
}