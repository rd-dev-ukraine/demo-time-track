using System.Collections.Generic;

namespace LanceTrack.Server.Cqrs.ProjectTime
{
    public interface IProjectTimeAggregateRootEventAccessor
    {
        IEnumerable<ProjectTimeTrackedEvent> ReadProjectTimeTrackedEvents(int projectId);

        void Append(ProjectTimeTrackedEvent @event);
    }
}