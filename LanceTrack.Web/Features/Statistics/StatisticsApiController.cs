using System.Linq;
using System.Web.Http;
using LanceTrack.Domain.Projects;
using LanceTrack.Domain.UserAccounts;
using LanceTrack.Web.Features.TrackTime.Models;

namespace LanceTrack.Web.Features.Statistics
{
    [Authorize, RoutePrefix("api/statistics")]
    public class StatisticsApiController : ApiController
    {
        private readonly UserAccount _currentUser;
        private readonly IProjectService _projectService;

        public StatisticsApiController(UserAccount currentUser, IProjectService projectService)
        {
            _currentUser = currentUser;
            _projectService = projectService;
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
    }
}
