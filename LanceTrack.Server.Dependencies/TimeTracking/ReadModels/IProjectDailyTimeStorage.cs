using LanceTrack.Domain.ProjectTime;

namespace LanceTrack.Server.Dependencies.TimeTracking.ReadModels
{
    public interface IProjectDailyTimeStorage
    {
        void SaveProjectDailyTime(ProjectDailyTime projectDailyTime);
    }
}