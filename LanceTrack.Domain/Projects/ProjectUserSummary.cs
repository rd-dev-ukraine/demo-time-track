using LinqToDB.Mapping;
using TypeLite;

namespace LanceTrack.Domain.Projects
{
    [Table("ProjectUserSummaryData"), TsClass(Module = "Api")]
    public class ProjectUserSummary
    {
        [Identity]
        public int Id { get; set; }

        [PrimaryKey]
        public int ProjectId { get; set; }

        [PrimaryKey]
        public int UserId { get; set; }

        [Column]
        public decimal ProjectTotalHoursReported { get; set; }

        [Column]
        public decimal UserTotalHoursReported { get; set; }

        [Column]
        public decimal ProjectTotalAmountEarned { get; set; }

        [Column]
        public decimal UserTotalAmountEarned { get; set; }
    }
}