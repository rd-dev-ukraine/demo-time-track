using System;
using System.Linq;

namespace LanceTrack.Server.Dependencies.Project
{
    public interface IProjectAccessor
    {
        Domain.ProjectTime.Project GetById(int id);

        IQueryable<Domain.ProjectTime.Project> GetReportableProjectsForUser(int userId, DateTime startDate, DateTime endDate);
    }
}