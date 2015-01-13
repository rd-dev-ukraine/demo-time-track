using System.Collections.Generic;

namespace LanceTrack.Domain.UserAccounts
{
    public interface IUserService
    {
        IEnumerable<UserAccount> All();
    }
}