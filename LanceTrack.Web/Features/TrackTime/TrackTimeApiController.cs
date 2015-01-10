using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web.Http;
using LanceTrack.Domain.ProjectTime;
using LanceTrack.Domain.TimeTracking;
using LanceTrack.Domain.UserAccounts;
using LanceTrack.Server;
using LanceTrack.Web.Features.TrackTime.Models;

namespace LanceTrack.Web.Features.TrackTime
{
    [RoutePrefix("api/tracker")]
    public class TrackTimeApiController : ApiController
    {
        private readonly UserAccount _currentUser;
        private readonly IProjectTimeService _projectTimeService;
        private readonly ITimeTrackingService _timeTrackingService;

        public TrackTimeApiController(UserAccount currentUser, IProjectTimeService projectTimeService, ITimeTrackingService timeTrackingService)
        {
            if (projectTimeService == null)
                throw new ArgumentNullException("projectTimeService");
            if (timeTrackingService == null)
                throw new ArgumentNullException("timeTrackingService");
            if (currentUser == null)
                throw new ArgumentNullException("currentUser");

            _projectTimeService = projectTimeService;
            _timeTrackingService = timeTrackingService;
            _currentUser = currentUser;
        }

        [Route("project-time/{weekDate?}", Name = "ProjectTimeInfo"), HttpGet]
        public ProjectTimeInfoResult ProjectTimeInfo(DateTime weekDate)
        {
            var startDateVal = weekDate.StartOfWeek();
            var endDateVal = weekDate.EndOfWeek();

            return new ProjectTimeInfoResult
            {
                StartDate = startDateVal,
                EndDate = endDateVal,
                Time = _projectTimeService.GetProjectTimeInfo(startDateVal, endDateVal).ToList()
            };
        }

        [Route("track", Name = "TrackTime"), HttpPost]
        public IHttpActionResult TrackTime(TrackTimeParams parameters)
        {
            try
            {
                _timeTrackingService.TrackTime(parameters.ProjectId , _currentUser.Id, parameters.At, parameters.Hours);
                return Ok();
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("recalculate", Name = "RecalculateAll"), HttpPost]
        public void RecalculateAll()
        {
            _timeTrackingService.RecalculateAll();
        }

        public class TrackTimeParams
        {
            public int ProjectId { get; set; }

            public DateTime At { get; set; }

            public decimal Hours { get; set; }
        }
    }
}
