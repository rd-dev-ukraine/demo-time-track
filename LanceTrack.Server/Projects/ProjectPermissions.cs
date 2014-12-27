using System;

namespace LanceTrack.Server.Projects
{
    [Flags]
    public enum ProjectPermissions
    {
        None = 0,
        View = 1,
        TrackSelf = 2,
        TrackAsOtherUser = 4
    }
}