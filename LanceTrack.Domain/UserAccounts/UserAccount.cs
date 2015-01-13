using BLToolkit.DataAccess;
using TypeLite;

namespace LanceTrack.Domain.UserAccounts
{
    [TableName("UserAccount"), TsClass(Module = "Api")]
    public class UserAccount
    {
        [PrimaryKey, Identity]
        public int Id { get; set; }

        public string Email { get; set; }

        public string DisplayName { get; set; }
    }
}