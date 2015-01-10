using System;
using System.Collections.Generic;

namespace LanceTrack.Server.Dependencies.ProjectDailyTime
{
    public interface IProjectTimeRepository
    {
        IEnumerable<ProjectDailyTimeData> GetProjectDailyTime(int projectId, int userId, DateTime startDate, DateTime endDate);
    }
}