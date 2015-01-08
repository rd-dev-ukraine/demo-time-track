using System;
using System.Collections.Generic;
using System.Linq;
using LanceTrack.Domain.ProjectUserInfo;
using LanceTrack.Domain.UserAccounts;
using LanceTrack.Server.Dependencies.Project;
using LanceTrack.Server.Dependencies.ProjectUserInfo;

namespace LanceTrack.Server.ProjectUserInfo
{
    public class ProjectUserInfoService : IProjectUserInfoService
    {
        private readonly UserAccount _currentUser;
        private readonly IProjectService _projectService;
        private readonly IProjectUserSummaryAccessor _projectUserSummaryAccessor;

        public ProjectUserInfoService(
            UserAccount currentUser, 
            IProjectService projectService, 
            IProjectUserSummaryAccessor projectUserSummaryAccessor)
        {
            if (currentUser == null)
                throw new ArgumentNullException("currentUser");
            if (projectService == null)
                throw new ArgumentNullException("projectService");
            if (projectUserSummaryAccessor == null)
                throw new ArgumentNullException("projectUserSummaryAccessor");

            _currentUser = currentUser;
            _projectService = projectService;
            _projectUserSummaryAccessor = projectUserSummaryAccessor;
        }


        public ProjectUserSummaryData ProjectUserSummary(int projectId)
        {
            var result = _projectUserSummaryAccessor.ProjectUserSummary(_currentUser.Id, projectId);
            var projectData = _projectService.GetProjectUserData(_currentUser.Id, projectId);

            if (projectData == null)
                return null;

            if ((projectData.UserPermissions & ProjectPermissions.ViewTotalAmount) == 0)
                result.ProjectTotalAmountEarned = 0;
            if ((projectData.UserPermissions & ProjectPermissions.ViewProjectTotalHours) == 0)
                result.ProjectTotalHoursReported = 0;

            return result;
        }

        public IEnumerable<ProjectUserSummaryData> AllProjectUserSummaryData()
        {
            return _projectService.GetProjects()
                                  .Select(p => ProjectUserSummary(p.Id))
                                  .Where(r => r != null)
                                  .ToArray();
        }
    }
}