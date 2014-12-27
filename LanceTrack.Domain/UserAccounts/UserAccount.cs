using BLToolkit.DataAccess;

namespace LanceTrack.Domain.UserAccounts
{
    public class UserAccount
    {
        [PrimaryKey, Identity]
        public int Id { get; set; }

        public string Email { get; set; }
    }
}