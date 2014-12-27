﻿namespace LanceTrack.Domain.UserAccounts
{
    public interface IUserAccountRepository
    {
        UserAccount Login(string email, string password);

        void CheckUserAuthorizedForProject(int userId, int projectId);
    }
}