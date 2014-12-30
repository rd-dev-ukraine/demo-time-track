using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using LanceTrack.Domain.ProjectTime;

namespace LanceTrack.Web.Features.TrackTime
{
    [RoutePrefix("api/tracktime")]
    public class TrackTimeApiController : ApiController
    {
        private readonly IProjectTimeService _projectTimeService;

        public TrackTimeApiController(IProjectTimeService projectTimeService)
        {
            _projectTimeService = projectTimeService;
        }

        [Route(""), HttpGet]
        public List<ProjectTimeInfo> ProjectTimeInfo()
        {
            return _projectTimeService.GetProjectTimeInfo(DateTime.Now.AddDays(-3), DateTime.Now.AddDays(4))
                                      .ToList();
        }
    }
}
