using System;
using LanceTrack.Domain.UserAccounts;
using LanceTrack.Server.Dependencies.UserAccounts;

namespace LanceTrack.Server.UserAccounts
{
    public class UserAccountService : IUserAccountService
    {
        private readonly IUserAccountRepository _userAccountRepository;

        public UserAccountService(IUserAccountRepository userAccountRepository)
        {
            if (userAccountRepository == null)
                throw new ArgumentNullException("userAccountRepository");

            _userAccountRepository = userAccountRepository;
        }

        public UserAccount Login(string email, string password)
        {
            if (String.IsNullOrWhiteSpace(email))
                throw new ArgumentNullException("email");
            if (String.IsNullOrWhiteSpace(password))
                throw new ArgumentNullException("password");

            var user = _userAccountRepository.FindByCredentials(email, password);
            if (user == null)
                throw new LoginFailedException();

            return user;
        }

        public UserAccount FindByEmail(string email)
        {
            if (String.IsNullOrWhiteSpace(email))
                throw new ArgumentNullException("email");

            return _userAccountRepository.FindByEmail(email);
        }
    }
}