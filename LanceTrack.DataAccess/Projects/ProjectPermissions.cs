using BLToolkit.DataAccess;

namespace LanceTrack.DataAccess.Projects
{
    [TableName("ProjectPermissions")]
    class ProjectPermissions
    {
        [Identity]
        public int Id { get; set; }

        [PrimaryKey]
        public int ProjectId { get; set; }

        [PrimaryKey]
        public int UserId { get; set; }

        public Server.Projects.ProjectPermissions UserPermissions { get; set; }
    }
}