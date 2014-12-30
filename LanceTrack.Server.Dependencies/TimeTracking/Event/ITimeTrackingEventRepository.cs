using System.Collections.Generic;

namespace LanceTrack.Server.Dependencies.TimeTracking.Event
{
    public interface ITimeTrackingEventRepository
    {
        IEnumerable<ProjectTimeTrackedEvent> ReadTimeTrackedEvents(int projectId);

        void StoreEvent(ProjectTimeTrackedEvent @event);
    }
}