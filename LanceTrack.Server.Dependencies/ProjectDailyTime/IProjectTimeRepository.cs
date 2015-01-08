using System;
using System.Linq;

namespace LanceTrack.Server.Dependencies.ProjectDailyTime
{
    public interface IProjectTimeRepository
    {
        IQueryable<ProjectDailyTimeData> GetProjectDailyTime(int projectId, int userId, DateTime startDate, DateTime endDate);
    }
}