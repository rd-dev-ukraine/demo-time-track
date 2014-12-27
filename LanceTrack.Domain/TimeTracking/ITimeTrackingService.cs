using System;

namespace LanceTrack.Domain.TimeTracking
{
    public interface ITimeTrackingService
    {
        void TrackTime(int projectId, DateTime time, string description);
    }
}