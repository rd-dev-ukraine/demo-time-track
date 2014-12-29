using System;
using System.Linq;
using LanceTrack.Domain.Projects;

namespace LanceTrack.Server.Projects
{
    public interface IProjectAccessor
    {
        Project GetById(int id);

        IQueryable<Project> GetActiveProjects(int userId, DateTime startDate, DateTime endDate);
    }
}