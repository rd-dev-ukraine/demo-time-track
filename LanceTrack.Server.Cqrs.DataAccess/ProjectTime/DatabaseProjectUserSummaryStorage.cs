using System;
using System.Linq;
using BLToolkit.Data;
using BLToolkit.Data.Linq;
using LanceTrack.Domain.Projects;
using LanceTrack.Server.Cqrs.ProjectTime.Dependencies;

namespace LanceTrack.Server.Cqrs.DataAccess.ProjectTime
{
    public class DatabaseProjectUserSummaryStorage : IProjectUserSummaryStorage
    {
        public DatabaseProjectUserSummaryStorage(DbManager dbManager)
        {
            if (dbManager == null)
                throw new ArgumentNullException("dbManager");

            DbManager = dbManager;
        }

        private DbManager DbManager { get; set; }

        public void Save(ProjectUserSummary entity)
        {
            if (entity == null)
                throw new ArgumentNullException("entity");

            if (DbManager.GetTable<ProjectUserSummary>()
                         .Any(e => e.ProjectId == entity.ProjectId && e.UserId == entity.UserId))
                DbManager.Update(entity);
            else
                entity.Id = Convert.ToInt32(DbManager.InsertWithIdentity(entity));
        }
    }
}