using System.Collections.Generic;

namespace LanceTrack.Server.TimeTracking.Events
{
    public interface ITimeTrackingEventRepository
    {
        IEnumerable<TimeTrackedEvent> ReadTimeTrackedEvents(int projectId);

        void StoreEvent(TimeTrackedEvent @event);
    }
}