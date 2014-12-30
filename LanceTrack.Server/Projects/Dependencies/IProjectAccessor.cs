using System;
using System.Linq;
using LanceTrack.Domain.Projects;
using LanceTrack.Domain.ProjectTime;

namespace LanceTrack.Server.Projects
{
    public interface IProjectAccessor
    {
        Project GetById(int id);

        IQueryable<Project> GetReportableProjectsForUser(int userId, DateTime startDate, DateTime endDate);
    }
}