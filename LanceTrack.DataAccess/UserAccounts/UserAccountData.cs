using LanceTrack.Domain.UserAccounts;

namespace LanceTrack.Server.DataAccess.UserAccounts
{
    public class UserAccountData : UserAccount
    {
        public string Password { get; set; }
    }
}