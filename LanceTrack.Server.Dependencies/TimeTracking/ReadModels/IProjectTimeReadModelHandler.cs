using LanceTrack.Server.Dependencies.Cqrs.ProjectTime;

namespace LanceTrack.Server.Dependencies.TimeTracking.ReadModels
{
    public interface IProjectTimeReadModelHandler
    {
        void AppyEvent(ProjectTimeTrackedEvent evt);

        void Save();
    }
}