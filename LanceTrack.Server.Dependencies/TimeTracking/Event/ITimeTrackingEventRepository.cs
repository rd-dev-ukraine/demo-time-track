using System.Collections.Generic;

namespace LanceTrack.Server.Dependencies.TimeTracking.Event
{
    public interface ITimeTrackingEventRepository
    {
        IEnumerable<TimeTrackedEvent> ReadTimeTrackedEvents(int projectId);

        void StoreEvent(TimeTrackedEvent @event);
    }
}