using LanceTrack.Domain.UserAccounts;

namespace LanceTrack.Server.UserAccounts
{
    public interface IUserAccountDataAccessor
    {
        UserAccount FindByCredentials(string email, string password);

        UserAccount GetById(int id);
    }
}