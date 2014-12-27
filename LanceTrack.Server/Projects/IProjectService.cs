using LanceTrack.Domain.Projects;

namespace LanceTrack.Server.Projects
{
    public interface IProjectService
    {
        Project GetById(int id);
    }
}