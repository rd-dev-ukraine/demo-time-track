﻿using System;
using System.Collections.Generic;
using System.Linq;
using LanceTrack.Domain.ProjectTime;
using LanceTrack.Domain.UserAccounts;
using LanceTrack.Server.Dependencies.ProjectTime;
using LanceTrack.Server.Projects;

namespace LanceTrack.Server.ProjectTime
{
    public class ProjectTimeService : IProjectTimeService
    {
        private readonly UserAccount _currentUser;
        private readonly ProjectService _projectService;
        private readonly IProjectTimeRepository _projectTimeRepository;

        public ProjectTimeService(
            UserAccount currentUser,
            ProjectService projectService,
            IProjectTimeRepository projectTimeRepository)
        {
            if (currentUser == null)
                throw new ArgumentNullException("currentUser");
            if (projectService == null)
                throw new ArgumentNullException("projectService");
            if (projectTimeRepository == null)
                throw new ArgumentNullException("projectTimeRepository");

            _currentUser = currentUser;
            _projectService = projectService;
            _projectTimeRepository = projectTimeRepository;
        }

        public IEnumerable<ProjectTimeInfo> GetProjectTimeInfo(DateTime startDate, DateTime endDate)
        {
            var projects = _projectService.GetReportableProjects(startDate, endDate).ToList();
            return projects.Select(a => new ProjectTimeInfo
                           {
                               ProjectId = a.Id,
                               ProjectTitle = a.Name,
                               Time = _projectTimeRepository.GetProjectDailyTime(a.Id, _currentUser.Id, startDate, endDate)
                                                            .Select(time => new TimeRecord
                                                            {
                                                                Date = time.Date.Date,
                                                                Hours = time.TotalHours
                                                            })
                                                            .ToList()
                           })
                           .ToList();
        }
    }
}