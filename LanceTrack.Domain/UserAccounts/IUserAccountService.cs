namespace LanceTrack.Domain.UserAccounts
{
    public interface IUserAccountService
    {
        UserAccount Login(string email, string password);

        UserAccount FindByEmail(string email);
    }
}