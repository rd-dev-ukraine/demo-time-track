using System;
using System.Collections.Generic;
using System.Data;
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
            return _projectRepository.GetProjectUserData()
                                     .SingleOrDefault(d => d.UserId == userId && d.ProjectId == projectId);
        }

        public IEnumerable<ProjectDailyTime> ProjectDailyTime(DateTime startDate, DateTime endDate)
        {
            return _projectRepository.ReportableProjects(_currentUser.Id)
                                     .SelectMany(p => ProjectDailyTime(p.Id, startDate, endDate))
                                     .ToList();
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

        private IEnumerable<ProjectDailyTime> ProjectDailyTime(int projectId, DateTime startDate, DateTime endDate)
        {
            var perms = _projectRepository.GetProjectUserData().SingleOrDefault(d => d.UserId == _currentUser.Id && d.ProjectId == projectId);
            if (perms == null)
                return Enumerable.Empty<ProjectDailyTime>();

            var project = _projectRepository.ReportableProjects(_currentUser.Id).SingleOrDefault(p => p.Id == projectId);
            if (project == null)
                return Enumerable.Empty<ProjectDailyTime>();

            startDate = project.StartDate.Date > startDate.Date ? project.StartDate.Date : startDate.Date;
            endDate = project.EndDate.HasValue && project.EndDate.Value.Date < endDate ? project.EndDate.Value.Date : endDate.Date;

            var canReportForOtherUsers = (perms.UserPermissions & ProjectPermissions.TrackAsOtherUser) != 0;

            var projectUsers = _projectRepository.GetProjectUserData()
                                                 .Where(p => p.ProjectId == projectId && (p.UserId == _currentUser.Id || canReportForOtherUsers));

            var filledTime = _projectRepository.GetProjectDailyTime(startDate, endDate)
                                               .Where(p => p.ProjectId == projectId && (p.UserId == _currentUser.Id || canReportForOtherUsers))
                                               .ToList();

            var dates = DatesRange(startDate, endDate).ToArray();
            return projectUsers.SelectMany(pu => dates.Select(d => filledTime.SingleOrDefault(time => time.ProjectId == pu.ProjectId && 
                                                                                                      time.Date.Date == d.Date && 
                                                                                                      time.UserId == pu.UserId) ??
                                                                  new ProjectDailyTime 
                                                                  {
                                                                      Date = d.Date,
                                                                      ProjectId = pu.ProjectId,
                                                                      UserId = pu.UserId
                                                                  }))
                               .ToArray();
        }

        private static IEnumerable<DateTime> DatesRange(DateTime startDate, DateTime endDate)
        {
            while (startDate.Date <= endDate.Date)
            {
                yield return startDate.Date;
                startDate = startDate.AddDays(1);
            }
        }
    }
}