using System.Linq;
using LanceTrack.Domain.UserAccounts;

namespace LanceTrack.Server.Dependencies.UserAccounts
{
    public interface IUserAccountRepository
    {
        UserAccount FindByCredentials(string email, string password);

        UserAccount GetById(int id);

        UserAccount FindByEmail(string email);

        IQueryable<UserAccount> All(int userId);
    }
}