using System;
using LinqToDB.Mapping;
using TypeLite;

namespace LanceTrack.Domain.Projects
{
    [Table("Project"), TsClass(Module = "Api")]
    public class ProjectBase
    {
        [PrimaryKey, Identity]
        public int Id { get; set; }

        [Column]
        public string Name { get; set; }

        [Column]
        public ProjectStatus Status { get; set; }

        [Column]
        public DateTimeOffset StartDate { get; set; }

        [Column]
        public DateTimeOffset? EndDate { get; set; }

        [Column]
        public int? MaxTotalHours { get; set; }
    }
}