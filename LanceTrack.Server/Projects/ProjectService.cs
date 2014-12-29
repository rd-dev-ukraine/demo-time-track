using System;
using System.Linq;
using LanceTrack.Domain.Projects;
using LanceTrack.Domain.UserAccounts;

namespace LanceTrack.Server.Projects
{
    public class ProjectService : IProjectService, Domain.Projects.IProjectService
    {
        private readonly IProjectAccessor _projectAccessor;
        private readonly UserAccount _currentUser;

        public ProjectService(IProjectAccessor projectAccessor, UserAccount currentUser)
        {
            if (projectAccessor == null)
                throw new ArgumentNullException("projectAccessor");
            if (currentUser == null)
                throw new ArgumentNullException("currentUser");

            _projectAccessor = projectAccessor;
            _currentUser = currentUser;
        }

        public Project GetById(int id)
        {
            return _projectAccessor.GetById(id);
        }

        public IQueryable<Project> GetActiveProjects(DateTime startDate, DateTime endDate)
        {
            return _projectAccessor.GetActiveProjects(_currentUser.Id, startDate, endDate);
        }
    }
}