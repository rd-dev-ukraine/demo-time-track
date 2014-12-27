using System;

namespace LanceTrack.Domain.TimeTracking
{
    public interface ITimeTrackingService
    {
        void TrackTime(int projectId, int userId, DateTime at, decimal hours);
    }
}