using System.Collections.Generic;
using LanceTrack.Server.Dependencies.Cqrs.ProjectTime;

namespace LanceTrack.Server.Dependencies.TimeTracking.Event
{
    public interface ITimeTrackingEventRepository
    {
        IEnumerable<ProjectTimeTrackedEvent> ReadTimeTrackedEvents(int projectId);

        void StoreEvent(ProjectTimeTrackedEvent @event);

        IEnumerable<IProjectEvent> All();
    }
}