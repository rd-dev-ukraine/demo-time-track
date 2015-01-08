using LanceTrack.Domain.ProjectUserInfo;

namespace LanceTrack.Server.Cqrs.ProjectTime.Dependencies
{
    public interface IProjectUserSummaryStorage
    {
        void Save(ProjectUserSummaryData entity);
    }
}