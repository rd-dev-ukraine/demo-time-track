using LanceTrack.Domain.ProjectTime;

namespace LanceTrack.Server.TimeTracking.ReadModels
{
    public interface IProjectDailyTimeStorage
    {
        void SaveProjectDailyTime(ProjectDailyTime projectDailyTime);
    }
}