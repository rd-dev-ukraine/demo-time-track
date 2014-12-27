using System;
using System.Linq;
using BLToolkit.Data;
using LanceTrack.Server.Projects;

namespace LanceTrack.DataAccess.Projects
{
    public class DatabaseProjectPermissionsAccessor : IProjectPermissionsAccessor
    {
        public DatabaseProjectPermissionsAccessor(DbManager dbManager)
        {
            if (dbManager == null)
                throw new ArgumentNullException("dbManager");

            DbManager = dbManager;
        }

        private DbManager DbManager { get; set; }

        public Server.Projects.ProjectPermissions GetProjectPermissionsForUser(int userId, int projectId)
        {
            var perms = DbManager.GetTable<ProjectPermissions>().SingleOrDefault(pp => pp.UserId == userId && pp.ProjectId == projectId);
            if (perms == null)
                return Server.Projects.ProjectPermissions.None;

            return perms.UserPermissions;
        }
    }
}