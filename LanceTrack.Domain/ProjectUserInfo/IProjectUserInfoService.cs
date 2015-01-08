using System.Collections.Generic;

namespace LanceTrack.Domain.ProjectUserInfo
{
    public interface IProjectUserInfoService
    {
        ProjectUserSummaryData ProjectUserSummary(int projectId);

        IEnumerable<ProjectUserSummaryData> AllProjectUserSummaryData(); 
    }
}