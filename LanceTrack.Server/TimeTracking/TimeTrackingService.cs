using System;
using LanceTrack.Cqrs.Contract;
using LanceTrack.Domain.TimeTracking;
using LanceTrack.Domain.UserAccounts;
using LanceTrack.Server.Cqrs.ProjectTime.Commands;

namespace LanceTrack.Server.TimeTracking
{
    public class TimeTrackingService : ITimeTrackingService
    {
        private readonly ICqrs _cqrs;
        private readonly UserAccount _currentUser;

        public TimeTrackingService(UserAccount currentUser)
        {
            _currentUser = currentUser;
        }

        public void RecalculateAll()
        {
            throw new NotImplementedException();
        }

        public void TrackTime(int projectId, int userId, DateTime at, decimal hours)
        {
            var command = new TrackTimeCommand
            {
                At = at,
                Hours = hours,
                ProjectId = projectId,
                UserId = _currentUser.Id
            };
            _cqrs.Execute(command);
        }
    }
}