using LanceTrack.Server.Dependencies.TimeTracking.Event;

namespace LanceTrack.Server.Dependencies.TimeTracking.ReadModels
{
    public interface IProjectTimeReadModel
    {
        void AppyEvent(TimeTrackedEvent evt);

        void Save();
    }
}