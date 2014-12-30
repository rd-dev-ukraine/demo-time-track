using LanceTrack.Domain.UserAccounts;

namespace LanceTrack.Server.Dependencies.UserAccounts
{
    public interface IUserAccountDataAccessor
    {
        UserAccount FindByCredentials(string email, string password);

        UserAccount GetById(int id);

        UserAccount FindByEmail(string email);
    }
}