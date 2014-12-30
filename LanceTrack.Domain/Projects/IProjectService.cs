using System;
using System.Linq;

namespace LanceTrack.Domain.Projects
{
    public interface IProjectService
    {
        IQueryable<Project> GetReportableProjects(DateTime startDate, DateTime endDate);
    }
}