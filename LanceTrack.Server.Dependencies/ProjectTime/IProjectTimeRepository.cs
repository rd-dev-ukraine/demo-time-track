using System;
using System.Linq;
using LanceTrack.Server.Dependencies.Project;

namespace LanceTrack.Server.Dependencies.ProjectTime
{
    public interface IProjectTimeRepository
    {
        IQueryable<ProjectDailyTime> GetProjectDailyTime(int projectId, int userId, DateTime startDate, DateTime endDate);
    }
}