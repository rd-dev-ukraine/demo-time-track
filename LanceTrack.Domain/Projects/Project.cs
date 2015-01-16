using BLToolkit.Mapping;
using TypeLite;

namespace LanceTrack.Domain.Projects
{
    [TsClass(Module = "Api", Name = "Project")]
    public class Project : ProjectBase
    {
        [MapIgnore]
        public ProjectPermissions Permissions { get; set; }
    }
}