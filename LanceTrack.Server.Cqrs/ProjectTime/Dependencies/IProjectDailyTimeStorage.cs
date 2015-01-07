using LanceTrack.Server.Cqrs.ProjectTime.ReadModels;
using LanceTrack.Server.Dependencies.ProjectDailyTime;

namespace LanceTrack.Server.Cqrs.ProjectTime.Dependencies
{
    public interface IProjectDailyTimeStorage
    {
        void SaveProjectDailyTime(ProjectDailyTime readModel);
    }
}