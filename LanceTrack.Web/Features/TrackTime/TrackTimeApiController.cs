using System;
using System.Linq;
using System.Web.Http;
using LanceTrack.Domain.Projects;
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
        private readonly IProjectService _projectService;
        private readonly ITimeTrackingService _timeTrackingService;
        private readonly IUserService _userService;

        public TrackTimeApiController(
            UserAccount currentUser,
            ITimeTrackingService timeTrackingService,
            IUserService userService,
            IProjectService projectService)
        {
            if (timeTrackingService == null)
                throw new ArgumentNullException("timeTrackingService");
            if (currentUser == null)
                throw new ArgumentNullException("currentUser");
            if (userService == null)
                throw new ArgumentNullException("userService");
            if (projectService == null)
                throw new ArgumentNullException("projectService");


            _timeTrackingService = timeTrackingService;
            _userService = userService;
            _projectService = projectService;
            _currentUser = currentUser;
        }

        [Route("project-time/{weekDate?}", Name = "ProjectTimeInfo"), HttpGet]
        public ProjectTimeInfoResult ProjectTimeInfo(DateTime weekDate)
        {
            var startDateVal = weekDate.StartOfWeek();
            var endDateVal = weekDate.EndOfWeek();

            return new ProjectTimeInfoResult
            {
                CurrentUserId = _currentUser.Id,
                StartDate = startDateVal,
                EndDate = endDateVal,
                Projects = _projectService.ReportableProjects().ToList(),
                Time = _projectService.ProjectDailyTime(startDateVal, endDateVal).ToList(),
                Users = _userService.All().ToList()
            };
        }

        [Route("statistics", Name = "Statistics"), HttpGet]
        public StatisticsResult Statistics()
        {
            var stats = _projectService.ProjectUserSummary();

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

                _timeTrackingService.TrackTime(parameters.ProjectId, parameters.UserId, parameters.At, hours);
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
            public int UserId { get; set; }
        }
    }
}