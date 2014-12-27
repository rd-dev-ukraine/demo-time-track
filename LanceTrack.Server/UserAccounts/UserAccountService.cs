using System;
using LanceTrack.Domain.UserAccounts;

namespace LanceTrack.Server.UserAccounts
{
    public class UserAccountService : IUserAccountService
    {
        private readonly IUserAccountDataAccessor _userAccountDataAccessor;

        public UserAccountService(IUserAccountDataAccessor userAccountDataAccessor)
        {
            if (userAccountDataAccessor == null)
                throw new ArgumentNullException("userAccountDataAccessor");

            _userAccountDataAccessor = userAccountDataAccessor;
        }

        public UserAccount Login(string email, string password)
        {
            if (String.IsNullOrWhiteSpace(email))
                throw new ArgumentNullException("email");
            if (String.IsNullOrWhiteSpace(password))
                throw new ArgumentNullException("password");

            var user = _userAccountDataAccessor.FindByCredentials(email, password);
            if (user == null)
                throw new LoginFailedException();

            return user;
        }
    }
}