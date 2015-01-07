using System;
using System.Linq;

namespace LanceTrack.Server.Dependencies.ProjectTime
{
    public interface IProjectTimeRepository
    {
        IQueryable<ProjectDailyTime> GetProjectDailyTime(int projectId, int userId, DateTime startDate, DateTime endDate);
    }
}