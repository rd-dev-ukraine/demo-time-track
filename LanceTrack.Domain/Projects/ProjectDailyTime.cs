using System;
using BLToolkit.DataAccess;
using TypeLite;

namespace LanceTrack.Domain.Projects
{
    [TableName("ProjectDailyTimeData"), TsClass(Module = "Api")]
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