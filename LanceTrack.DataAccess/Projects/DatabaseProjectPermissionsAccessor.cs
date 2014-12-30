using System;
using System.Linq;
using BLToolkit.Data;
using LanceTrack.Server.Dependencies.Project;

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

        public Server.Dependencies.Project.ProjectPermissions GetProjectPermissionsForUser(int userId, int projectId)
        {
            var perms = DbManager.GetTable<ProjectPermissions>().SingleOrDefault(pp => pp.UserId == userId && pp.ProjectId == projectId);
            if (perms == null)
                return LanceTrack.Server.Dependencies.Project.ProjectPermissions.None;

            return perms.UserPermissions;
        }
    }
}