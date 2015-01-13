using TypeLite;

namespace LanceTrack.Domain.Projects
{
    [TsEnum(Module = "Api")]
    public enum ProjectStatus
    {
        Disabled = 0,
        Active = 1,
        Completed = 2
    }
}