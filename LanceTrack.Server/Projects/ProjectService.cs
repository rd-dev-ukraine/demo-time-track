using System;
using System.Linq;
using LanceTrack.Domain.ProjectTime;
using LanceTrack.Domain.UserAccounts;
using LanceTrack.Server.Dependencies.Project;

namespace LanceTrack.Server.Projects
{
    public class ProjectService : IProjectTimeService
    {
        private readonly IProjectAccessor _projectAccessor;
        private readonly IProjectPermissionsAccessor _projectPermissionsAccessor;
        private readonly UserAccount _currentUser;

        public ProjectService(
            IProjectAccessor projectAccessor, 
            UserAccount currentUser, 
            IProjectPermissionsAccessor projectPermissionsAccessor)
        {
            if (projectAccessor == null)
                throw new ArgumentNullException("projectAccessor");
            if (currentUser == null)
                throw new ArgumentNullException("currentUser");
            if (projectPermissionsAccessor == null)
                throw new ArgumentNullException("projectPermissionsAccessor");

            _projectAccessor = projectAccessor;
            _currentUser = currentUser;
            _projectPermissionsAccessor = projectPermissionsAccessor;
        }

        public Project GetById(int id)
        {
            return _projectAccessor.GetById(id);
        }

        public IQueryable<Project> GetReportableProjects(DateTime startDate, DateTime endDate)
        {
            return _projectAccessor.GetReportableProjectsForUser(_currentUser.Id, startDate, endDate);
        }

        public ProjectPermissions CalculatePermissions(int userId, int projectId)
        {
            return _projectPermissionsAccessor.GetProjectPermissionsForUser(userId, projectId);
        }
    }
}