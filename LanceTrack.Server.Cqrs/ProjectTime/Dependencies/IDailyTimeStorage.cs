using LanceTrack.Server.Dependencies.ProjectDailyTime;

namespace LanceTrack.Server.Cqrs.ProjectTime.Dependencies
{
    public interface IDailyTimeStorage
    {
        void SaveProjectDailyTime(ProjectDailyTimeData readModel);
    }
}