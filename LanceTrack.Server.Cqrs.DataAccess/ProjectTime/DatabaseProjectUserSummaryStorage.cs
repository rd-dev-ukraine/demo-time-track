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
        public DatabaseProjectUserSummaryStorage(DataConnection db)
        {
            if (db == null)
                throw new ArgumentNullException("db");

            Db = db;
        }

        private DataConnection Db { get; set; }

        public void Save(ProjectUserSummary entity)
        {
            if (entity == null)
                throw new ArgumentNullException("entity");

            if (Db.GetTable<ProjectUserSummary>()
                         .Any(e => e.ProjectId == entity.ProjectId && e.UserId == entity.UserId))
                Db.Update(entity);
            else
                entity.Id = Convert.ToInt32(Db.InsertWithIdentity(entity));
        }
    }
}