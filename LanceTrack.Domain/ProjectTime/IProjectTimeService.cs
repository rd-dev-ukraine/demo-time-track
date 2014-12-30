using System;
using System.Collections.Generic;
using System.Linq;

namespace LanceTrack.Domain.ProjectTime
{
    public interface IProjectTimeService
    {
        IEnumerable<ProjectInfo> GetProjectTimeInfo(DateTime startDate, DateTime endDate);
    }
}