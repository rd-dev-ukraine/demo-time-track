using LinqToDB.Mapping;
using TypeLite;

namespace LanceTrack.Domain.Projects
{
    [TsClass(Module = "Api", Name = "Project")]
    public class Project : ProjectBase
    {
        [Column]
        public ProjectPermissions Permissions { get; set; }
    }
}