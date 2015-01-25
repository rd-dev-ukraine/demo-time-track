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
            DataConnection dbManager, 
            IProjectRepository projectRepository)
        {
            if (dbManager == null)
                throw new ArgumentNullException("dbManager");
            if (projectRepository == null)
                throw new ArgumentNullException("projectRepository");

            DbManager = dbManager;
            _projectRepository = projectRepository;
        }

        private DataConnection DbManager { get; set; }

        public UserAccount FindByCredentials(string email, string password)
        {
            var account = DbManager.GetTable<UserAccountData>().SingleOrDefault(ua => ua.Email == email && ua.Password == password);

            if (account == null)
                return null;

            return GetById(account.Id);
        }

        public UserAccount FindByEmail(string email)
        {
            return DbManager.GetTable<UserAccount>().SingleOrDefault(ua => ua.Email == email);
        }

        public IQueryable<UserAccount> All(int userId)
        {
            var projectWhereUserCouldSeeOtherUsers = _projectRepository.BillableProjects(userId).Union(_projectRepository.ReportableProjects(userId))
                                                                       .Select(d => d.Id);
            var projectUserData = DbManager.GetTable<ProjectUserInfo>();

            return DbManager.GetTable<UserAccount>()
                            .Where(ua => ua.Id == userId || 
                                         projectUserData.Any(d => d.UserId == ua.Id && 
                                                                  projectWhereUserCouldSeeOtherUsers.Contains(d.ProjectId)));
        }

        public UserAccount GetById(int id)
        {
            return DbManager.GetTable<UserAccount>().SingleOrDefault(u => u.Id == id);
        }
    }
}