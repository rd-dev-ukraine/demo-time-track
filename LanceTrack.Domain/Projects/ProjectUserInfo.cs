using BLToolkit.DataAccess;

namespace LanceTrack.Domain.Projects
{
    [TableName("ProjectUserInfo")]
    public class ProjectUserInfo
    {
        [Identity]
        public int Id { get; set; }

        [PrimaryKey]
        public int ProjectId { get; set; }

        [PrimaryKey]
        public int UserId { get; set; }

        public ProjectPermissions UserPermissions { get; set; }

        public decimal HourlyRate { get; set; }

        public decimal? MaxDailyHours { get; set; }

        public decimal? MaxProjectHours { get; set; }
    }
}