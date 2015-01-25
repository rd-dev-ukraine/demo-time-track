
using LinqToDB.Mapping;

namespace LanceTrack.Domain.Projects
{
    [Table("ProjectUserInfo")]
    public class ProjectUserInfo
    {
        [Identity]
        public int Id { get; set; }

        [PrimaryKey]
        public int ProjectId { get; set; }

        [PrimaryKey]
        public int UserId { get; set; }

        [Column]
        public ProjectPermissions UserPermissions { get; set; }

        [Column]
        public decimal HourlyRate { get; set; }

        [Column]
        public decimal? MaxDailyHours { get; set; }

        [Column]
        public decimal? MaxProjectHours { get; set; }
    }
}