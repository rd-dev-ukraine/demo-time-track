using LanceTrack.Domain.Projects;

namespace LanceTrack.Server.Cqrs.ProjectTime.Dependencies
{
    public interface IDailyTimeStorage
    {
        void SaveProjectDailyTime(ProjectDailyTime readModel);
    }
}