﻿using System;
using BLToolkit.DataAccess;

namespace LanceTrack.Domain.Projects
{
    [TableName("Project")]
    public class ProjectBase
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public ProjectStatus Status { get; set; }

        public DateTimeOffset StartDate { get; set; }

        public DateTimeOffset? EndDate { get; set; }

        public int? MaxTotalHoursPerDay { get; set; }

        public int? MaxTotalHours { get; set; }
    }
}