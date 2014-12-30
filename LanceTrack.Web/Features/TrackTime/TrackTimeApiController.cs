﻿using System;
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

        [Route("{startDate: datetime}-{endDate: datetime}"), HttpGet]
        public List<ProjectTimeInfo> ProjectTimeInfo(DateTime startDate, DateTime endDate)
        {
            return _projectTimeService.GetProjectTimeInfo(startDate, endDate).ToList();
        }
    }
}
