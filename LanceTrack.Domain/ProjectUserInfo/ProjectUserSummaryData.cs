﻿using BLToolkit.DataAccess;

namespace LanceTrack.Domain.ProjectUserInfo
{
    [TableName("ProjectUserSummaryData")]
    public class ProjectUserSummaryData
    {
        [Identity]
        public int Id { get; set; }

        [PrimaryKey]
        public int ProjectId { get; set; }

        [PrimaryKey]
        public int UserId { get; set; }

        public decimal ProjectTotalHoursReported { get; set; }

        public decimal UserTotalHoursReported { get; set; }

        public decimal ProjectTotalAmountEarned { get; set; }

        public decimal UserTotalAmountEarned { get; set; }
    }
}