using LinqToDB.Mapping;
using TypeLite;

namespace LanceTrack.Domain.UserAccounts
{
    [Table("UserAccount"), TsClass(Module = "Api")]
    public class UserAccount
    {
        [PrimaryKey, Identity]
        public int Id { get; set; }

        [Column]
        public string Email { get; set; }

        [Column]
        public string DisplayName { get; set; }
    }
}