using System;
using System.Collections.Generic;
using System.Linq;
using LanceTrack.Domain.UserAccounts;
using LanceTrack.Server.Dependencies.Project;

namespace LanceTrack.Server.Projects
{
    public class ProjectService : IProjectService
    {
        private readonly IProjectRepository _projectRepository;
        
        private readonly UserAccount _currentUser;

        public ProjectService(
            IProjectRepository projectRepository, 
            UserAccount currentUser)
        {
            if (projectRepository == null)
                throw new ArgumentNullException("projectRepository");
            if (currentUser == null)
                throw new ArgumentNullException("currentUser");
            _projectRepository = projectRepository;
            _currentUser = currentUser;
        }

        public IEnumerable<Project> GetProjects()
        {
            return _projectRepository.GetProjects(_currentUser.Id);
        }

        public Project GetById(int id)
        {
            return _projectRepository.GetById(id);
        }

        public IQueryable<Project> GetReportableProjects(DateTime startDate, DateTime endDate)
        {
            return _projectRepository.GetReportableProjectsForUser(_currentUser.Id, startDate, endDate);
        }

        public ProjectUserData GetProjectUserData(int userId, int projectId)
        {
            return _projectRepository.GetProjectPermissionsForUser(userId, projectId);
        }
    }
}