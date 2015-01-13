using BLToolkit.DataAccess;

namespace LanceTrack.Domain.UserAccounts
{
    [TableName("UserAccount")]
    public class UserAccount
    {
        [PrimaryKey, Identity]
        public int Id { get; set; }

        public string Email { get; set; }

        public string DisplayName { get; set; }
    }
}