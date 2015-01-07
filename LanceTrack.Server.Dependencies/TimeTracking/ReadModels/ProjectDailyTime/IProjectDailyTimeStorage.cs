namespace LanceTrack.Server.Dependencies.TimeTracking.ReadModels.ProjectDailyTime
{
    public interface IProjectDailyTimeStorage
    {
        void SaveProjectDailyTime(ProjectTime.ProjectDailyTime projectDailyTime);
    }
}