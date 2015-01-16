using System;
using TypeLite;

namespace LanceTrack.Domain.Projects
{
    [Flags, TsEnum(Module = "Api")]
    public enum ProjectPermissions
    {
        None = 0,
        View = 1,
        TrackSelf = 2,
        TrackAsOtherUser = 6, // Always includes track self
        ViewTotalAmount = 8,
        ViewProjectTotalHours = 16,
        BillProject = 32
    }
}