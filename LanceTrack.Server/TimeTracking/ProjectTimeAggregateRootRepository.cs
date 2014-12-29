using System;
using System.Collections.Generic;

namespace LanceTrack.Server.TimeTracking
{
    public class ProjectTimeAggregateRootRepository
    {
        private static readonly Dictionary<int, ProjectTimeAggregateRoot> AggregateRootCache = new Dictionary<int, ProjectTimeAggregateRoot>();

        private readonly Func<int, ProjectTimeAggregateRoot> _aggregateRootFactory;

        public ProjectTimeAggregateRootRepository(Func<int, ProjectTimeAggregateRoot> aggregateRootFactory)
        {
            _aggregateRootFactory = aggregateRootFactory;
        }

        public ProjectTimeAggregateRoot GetAggregateRoot(int projectId)
        {
            ProjectTimeAggregateRoot result;
            if (!AggregateRootCache.TryGetValue(projectId, out result))
            {
                result = _aggregateRootFactory(projectId);
                AggregateRootCache[projectId] = result;
            }

            return result; 
        }
    }
}
