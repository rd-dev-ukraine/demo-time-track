﻿using System;
using System.Linq;
using LanceTrack.Domain.Projects;

namespace LanceTrack.Server.Dependencies.Projects
{
    public interface IProjectRepository
    {
        IQueryable<Project> BillableProjects(int userId);
        ProjectBase GetById(int id);
        IQueryable<DailyTime> GetDailyTime(DateTime startDate, DateTime endDate);
        IQueryable<ProjectUserInfo> GetProjectUserInfo();
        IQueryable<ProjectUserSummary> ProjectUserSummary(int userId);
        IQueryable<Project> ReportableProjects(int userId);
    }
}