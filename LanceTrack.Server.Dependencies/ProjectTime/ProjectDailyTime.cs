using System;
using BLToolkit.DataAccess;

namespace LanceTrack.Server.Dependencies.ProjectTime
{
    [TableName("ProjectDailyTimeData")]
    public class ProjectDailyTime
    {
        [PrimaryKey]
        public int ProjectId { get;set; }

        [PrimaryKey]
        public int UserId { get; set; }

        [PrimaryKey]
        public DateTime Date { get; set; }

        public decimal TotalHours { get; set; }
    }
}