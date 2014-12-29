using LanceTrack.Server.TimeTracking.Events;

namespace LanceTrack.Server.TimeTracking.ReadModels
{
    public interface IProjectTimeReadModel
    {
        void AppyEvent(TimeTrackedEvent evt);

        void Save();
    }
}