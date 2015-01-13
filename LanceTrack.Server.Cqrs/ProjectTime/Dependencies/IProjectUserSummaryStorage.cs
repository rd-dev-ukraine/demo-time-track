using LanceTrack.Domain.Projects;

namespace LanceTrack.Server.Cqrs.ProjectTime.Dependencies
{
    public interface IProjectUserSummaryStorage
    {
        void Save(ProjectUserSummary entity);
    }
}