using System;
using System.Linq;
using BLToolkit.Data;
using BLToolkit.Data.Linq;
using LanceTrack.Domain.ProjectUserInfo;
using LanceTrack.Server.Cqrs.ProjectTime.Dependencies;

namespace LanceTrack.Server.Cqrs.DataAccess.ProjectTime
{
    public class ProjectUserSummaryStorage : IProjectUserSummaryStorage
    {
        public ProjectUserSummaryStorage(DbManager dbManager)
        {
            if (dbManager == null)
                throw new ArgumentNullException("dbManager");

            DbManager = dbManager;
        }

        private DbManager DbManager { get; set; }

        public void Save(ProjectUserSummaryData entity)
        {
            if (entity == null)
                throw new ArgumentNullException("entity");

            if (DbManager.GetTable<ProjectUserSummaryData>()
                         .Any(e => e.ProjectId == entity.ProjectId && e.UserId == entity.UserId))
                DbManager.Update(entity);
            else
                entity.Id = Convert.ToInt32(DbManager.InsertWithIdentity(entity));
        }
    }
}