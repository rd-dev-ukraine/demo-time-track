using System;
using System.Linq;
using LanceTrack.Domain.ProjectTime;
using LanceTrack.Domain.UserAccounts;

namespace LanceTrack.Server.Projects
{
    public class ProjectTimeService : IProjectTimeService
    {
        private readonly IProjectAccessor _projectAccessor;
        private readonly UserAccount _currentUser;

        public ProjectTimeService(IProjectAccessor projectAccessor, UserAccount currentUser)
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

        public IQueryable<Project> GetReportableProjects(DateTime startDate, DateTime endDate)
        {
            return _projectAccessor.GetReportableProjectsForUser(_currentUser.Id, startDate, endDate);
        }
    }
}