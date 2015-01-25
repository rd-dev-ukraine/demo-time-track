using System;
using System.Linq;
using LanceTrack.Domain.Projects;
using LanceTrack.Server.Cqrs.ProjectTime.Dependencies;
using LinqToDB;
using LinqToDB.Data;

namespace LanceTrack.Server.Cqrs.DataAccess.ProjectTime
{
    public class DatabaseProjectUserSummaryStorage : IProjectUserSummaryStorage
    {
        public DatabaseProjectUserSummaryStorage(DataConnection dbManager)
        {
            if (dbManager == null)
                throw new ArgumentNullException("dbManager");

            DbManager = dbManager;
        }

        private DataConnection DbManager { get; set; }

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