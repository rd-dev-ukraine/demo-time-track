using System;

namespace LanceTrack.Server.Projects
{
    public class ProjectPermissionsService : IProjectPermissionsService
    {
        private readonly IProjectPermissionsAccessor _projectPermissionsAccessor;

        public ProjectPermissionsService(IProjectPermissionsAccessor projectPermissionsAccessor)
        {
            if (projectPermissionsAccessor == null)
                throw new ArgumentNullException("projectPermissionsAccessor");

            _projectPermissionsAccessor = projectPermissionsAccessor;
        }

        public ProjectPermissions CalculatePermissions(int userId, int projectId)
        {
            return _projectPermissionsAccessor.GetProjectPermissionsForUser(userId, projectId);
        }
    }
}