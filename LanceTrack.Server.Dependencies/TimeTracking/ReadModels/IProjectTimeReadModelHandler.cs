using LanceTrack.Server.Dependencies.TimeTracking.Event;

namespace LanceTrack.Server.Dependencies.TimeTracking.ReadModels
{
    public interface IProjectTimeReadModelHandler
    {
        void AppyEvent(ProjectTimeTrackedEvent evt);

        void Save();
    }
}