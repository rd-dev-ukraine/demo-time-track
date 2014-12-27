using System.Collections.Generic;
using LanceTrack.Server.TimeTracking.Events;

namespace LanceTrack.Server.TimeTracking
{
    public interface ITimeTrackingEventRepository
    {
        IEnumerable<TimeTrackedEvent> ReadEvents(int projectId);

        void StoreEvent(TimeTrackedEvent @event);
    }
}