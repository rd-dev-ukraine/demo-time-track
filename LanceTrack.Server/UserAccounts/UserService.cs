using System;
using System.Collections.Generic;
using LanceTrack.Domain.UserAccounts;
using LanceTrack.Server.Dependencies.UserAccounts;

namespace LanceTrack.Server.UserAccounts
{
    public class UserService : IUserService
    {
        private readonly UserAccount _currentUser;
        private readonly IUserAccountRepository _userAccountRepository;

        public UserService(UserAccount currentUser, IUserAccountRepository userAccountRepository)
        {
            if (currentUser == null)
                throw new ArgumentNullException("currentUser");
            if (userAccountRepository == null)
                throw new ArgumentNullException("userAccountRepository");

            _currentUser = currentUser;
            _userAccountRepository = userAccountRepository;
        }

        public IEnumerable<UserAccount> All()
        {
            return _userAccountRepository.All(_currentUser.Id);
        }
    }
}