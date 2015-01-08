using System.Linq;
using LanceTrack.Domain.ProjectUserInfo;

namespace LanceTrack.Server.Dependencies.ProjectUserInfo
{
    public interface IProjectUserSummaryAccessor
    {
        ProjectUserSummaryData ProjectUserSummary(int userId, int projectId);
    }
}