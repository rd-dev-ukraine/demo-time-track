using System;
using System.Linq;
using System.Web.Http;
using LanceTrack.Domain.ProjectTime;
using LanceTrack.Domain.ProjectUserInfo;
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
        private readonly IProjectUserInfoService _projectUserInfoService;
        private readonly ITimeTrackingService _timeTrackingService;

        public TrackTimeApiController(
            UserAccount currentUser,
            IProjectTimeService projectTimeService,
            ITimeTrackingService timeTrackingService,
            IProjectUserInfoService projectUserInfoService)
        {
            if (projectTimeService == null)
                throw new ArgumentNullException("projectTimeService");
            if (timeTrackingService == null)
                throw new ArgumentNullException("timeTrackingService");
            if (currentUser == null)
                throw new ArgumentNullException("currentUser");
            if (projectUserInfoService == null)
                throw new ArgumentNullException("projectUserInfoService");

            _projectTimeService = projectTimeService;
            _timeTrackingService = timeTrackingService;
            _projectUserInfoService = projectUserInfoService;
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
                Projects = _projectTimeService.GetProjectTimeInfo(startDateVal, endDateVal).ToList()
            };
        }

        [Route("recalculate", Name = "RecalculateAll"), HttpPost]
        public void RecalculateAll()
        {
            _timeTrackingService.RecalculateAll();
        }

        [Route("statistics", Name = "Statistics"), HttpGet]
        public StatisticsResult Statistics()
        {
            var stats = _projectUserInfoService.AllProjectUserSummaryData();

            var result = new StatisticsResult
            {
                ProjectStatistics = stats.ToList()
            };

            if (stats.Any())
            {
                result.TotalEarnings = stats.Sum(p => p.UserTotalAmountEarned);
                result.TotalHours = stats.Sum(p => p.UserTotalHoursReported);
            }

            return result;
        }

        [Route("track", Name = "TrackTime"), HttpPost]
        public IHttpActionResult TrackTime(TrackTimeParams parameters)
        {
            try
            {
                decimal hours;
                if (!Decimal.TryParse(parameters.Hours, out hours))
                    return BadRequest("Hours value invalid.");

                _timeTrackingService.TrackTime(parameters.ProjectId, _currentUser.Id, parameters.At, hours);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        public class TrackTimeParams
        {
            public DateTime At { get; set; }
            public string Hours { get; set; }
            public int ProjectId { get; set; }
        }
    }
}