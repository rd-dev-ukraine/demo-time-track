using System;
using System.Collections.Generic;
using System.Linq;
using LanceTrack.Domain.Projects;
using LanceTrack.Domain.UserAccounts;
using LanceTrack.Server.Dependencies.Projects;

namespace LanceTrack.Server.Projects
{
    public class ProjectService : IProjectService
    {
        private readonly UserAccount _currentUser;
        private readonly IProjectRepository _projectRepository;

        public ProjectService(
            UserAccount currentUser,
            IProjectRepository projectRepository)
        {
            if (projectRepository == null)
                throw new ArgumentNullException("projectRepository");
            if (currentUser == null)
                throw new ArgumentNullException("currentUser");

            _projectRepository = projectRepository;
            _currentUser = currentUser;
        }

        public IEnumerable<Project> BillableProjects()
        {
            return _projectRepository.BillableProjects(_currentUser.Id).ToList();
        }

        public Project GetById(int id)
        {
            return _projectRepository.GetById(id);
        }

        public ProjectUserData GetProjectUserData(int userId, int projectId)
        {
            return _projectRepository.GetProjectPermissionsForUser(userId, projectId);
        }

        public IEnumerable<ProjectDailyTime> ProjectDailyTime(DateTime startDate, DateTime endDate)
        {
            return _projectRepository.GetProjectDailyTime(_currentUser.Id, startDate, endDate).ToList();
        }

        public IEnumerable<ProjectUserSummary> ProjectUserSummary()
        {
            var data = _projectRepository.ProjectUserSummary(_currentUser.Id)
                .ToList();

            foreach (var result in data)
            {
                var projectData = GetProjectUserData(_currentUser.Id, result.ProjectId);

                if (projectData == null)
                    return null;

                if ((projectData.UserPermissions & ProjectPermissions.ViewTotalAmount) == 0)
                    result.ProjectTotalAmountEarned = 0;
                if ((projectData.UserPermissions & ProjectPermissions.ViewProjectTotalHours) == 0)
                    result.ProjectTotalHoursReported = 0;
            }

            return data;
        }

        public IEnumerable<Project> ReportableProjects()
        {
            return _projectRepository.ReportableProjects(_currentUser.Id).ToList();
        }
    }
}