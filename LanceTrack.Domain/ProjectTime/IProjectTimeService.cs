using System;
using System.Linq;

namespace LanceTrack.Domain.ProjectTime
{
    public interface IProjectTimeService
    {
        Project GetById(int id);

        IQueryable<Project> GetReportableProjects(DateTime startDate, DateTime endDate);
    }
}