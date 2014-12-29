using System;
using System.Linq;
using BLToolkit.Data;
using LanceTrack.Domain.Projects;
using LanceTrack.Server.Projects;

namespace LanceTrack.DataAccess.Projects
{
    public class DatabaseProjectAccessor : IProjectAccessor
    {
        public DatabaseProjectAccessor(DbManager dbManager)
        {
            if (dbManager == null)
                throw new ArgumentNullException("dbManager");

            DbManager = dbManager;
        }

        private DbManager DbManager { get; set; } 

        public Project GetById(int id)
        {
            return DbManager.GetTable<Project>().SingleOrDefault(p => p.Id == id);
        }

        public IQueryable<Project> GetActiveProjects(int userId, DateTime startDate, DateTime endDate)
        {
            throw new NotImplementedException();
        }
    }
}