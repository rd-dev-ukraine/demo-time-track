using LanceTrack.Domain.UserAccounts;
using LinqToDB.Mapping;

namespace LanceTrack.Server.DataAccess.UserAccounts
{
    public class UserAccountData : UserAccount
    {
        [Column]
        public string Password { get; set; }
    }
}