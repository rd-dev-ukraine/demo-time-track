using System;
using System.Linq;
using LanceTrack.Domain.Projects;
using LanceTrack.Domain.UserAccounts;
using LanceTrack.Server.Dependencies.Projects;
using LanceTrack.Server.Dependencies.UserAccounts;
using LinqToDB.Data;

namespace LanceTrack.Server.DataAccess.UserAccounts
{
    public class DatabaseUserAccountRepository : IUserAccountRepository
    {
        private readonly IProjectRepository _projectRepository;

        public DatabaseUserAccountRepository(
            DataConnection db, 
            IProjectRepository projectRepository)
        {
            if (db == null)
                throw new ArgumentNullException("db");
            if (projectRepository == null)
                throw new ArgumentNullException("projectRepository");

            Db = db;
            _projectRepository = projectRepository;
        }

        private DataConnection Db { get; set; }

        public UserAccount FindByCredentials(string email, string password)
        {
            var account = Db.GetTable<UserAccountData>().SingleOrDefault(ua => ua.Email == email && ua.Password == password);

            if (account == null)
                return null;

            return GetById(account.Id);
        }

        public UserAccount FindByEmail(string email)
        {
            return Db.GetTable<UserAccount>().SingleOrDefault(ua => ua.Email == email);
        }

        public IQueryable<UserAccount> All(int userId)
        {
            var projectWhereUserCouldSeeOtherUsers = _projectRepository.BillableProjects(userId).Union(_projectRepository.ReportableProjects(userId))
                                                                       .Select(d => d.Id);
            var projectUserData = Db.GetTable<ProjectUserInfo>();

            return Db.GetTable<UserAccount>()
                            .Where(ua => ua.Id == userId || 
                                         projectUserData.Any(d => d.UserId == ua.Id && 
                                                                  projectWhereUserCouldSeeOtherUsers.Contains(d.ProjectId)));
        }

        public UserAccount GetById(int id)
        {
            return Db.GetTable<UserAccount>().SingleOrDefault(u => u.Id == id);
        }
    }
}