using System;
using System.Linq;
using BLToolkit.Data;
using LanceTrack.Domain.UserAccounts;
using LanceTrack.Server.UserAccounts;

namespace LanceTrack.DataAccess.UserAccounts
{
    public class DatabaseUserAccountAccessor : IUserAccountDataAccessor
    {
        public DatabaseUserAccountAccessor(DbManager dbManager)
        {
            if (dbManager == null)
                throw new ArgumentNullException("dbManager");

            DbManager = dbManager;
        }

        private DbManager DbManager { get; set; }

        public UserAccount FindByCredentials(string email, string password)
        {
            var account = DbManager.GetTable<UserAccountData>().SingleOrDefault(ua => ua.Email == email && ua.Password == password);

            if (account == null)
                return null;

            return GetById(account.Id);
        }

        public UserAccount GetById(int id)
        {
            return DbManager.GetTable<UserAccount>().SingleOrDefault(u => u.Id == id);
        }
    }
}