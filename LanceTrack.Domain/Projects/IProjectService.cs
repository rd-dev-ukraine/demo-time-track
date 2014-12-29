using System;
using System.Linq;

namespace LanceTrack.Domain.Projects
{
    public interface IProjectService
    {
        IQueryable<Project> GetActiveProjects(DateTime startDate, DateTime endDate);
    }
}