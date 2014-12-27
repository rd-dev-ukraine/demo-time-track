using System;
using LanceTrack.Domain.Projects;

namespace LanceTrack.Server.Projects
{
    public class ProjectService : IProjectService
    {
        private readonly IProjectAccessor _projectAccessor;

        public ProjectService(IProjectAccessor projectAccessor)
        {
            if (projectAccessor == null)
                throw new ArgumentNullException("projectAccessor");

            _projectAccessor = projectAccessor;
        }

        public Project GetById(int id)
        {
            return _projectAccessor.GetById(id);
        }
    }
}