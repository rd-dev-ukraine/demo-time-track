using System;
using System.Collections.Generic;

namespace LanceTrack.Domain.ProjectTime
{
    public interface IProjectTimeService
    {
        IEnumerable<ProjectTimeInfo> GetProjectTimeInfo(DateTime startDate, DateTime endDate);
    }
}