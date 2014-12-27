using LanceTrack.Domain.Projects;

namespace LanceTrack.Server.Projects
{
    public interface IProjectAccessor
    {
        Project GetById(int id);
    }
}