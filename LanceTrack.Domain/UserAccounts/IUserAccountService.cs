namespace LanceTrack.Domain.UserAccounts
{
    public interface IUserAccountService
    {
        UserAccount Login(string email, string password);
    }
}