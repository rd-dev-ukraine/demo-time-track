using System;
using System.Collections.Generic;

namespace LanceTrack.Domain.Projects
{
    public interface IProjectService
    {
        Project GetById(int id);

        IEnumerable<Project> BillableProjects();

        IEnumerable<ProjectDailyTime> ProjectDailyTime(DateTime startDate, DateTime endDate);

        /// <summary>
        ///     Gets the project summary for projects where user could report time.
        /// </summary>
        IEnumerable<ProjectUserSummary> ProjectUserSummary();

        /// <summary>
        /// Get the set of project where user could report time.
        /// </summary>
        IEnumerable<Project> ReportableProjects();

        ProjectUserData GetProjectUserData(int userId, int projectId);
    }
}