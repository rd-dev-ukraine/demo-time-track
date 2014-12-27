using System;
using System.Runtime.Serialization;

namespace LanceTrack.Domain.UserAccounts
{
    public class LoginFailedException : Exception
    {
        public LoginFailedException()
            :base(Messages.LoginFailed)
        {
        }

        public LoginFailedException(string message)
            : base(message)
        {
        }

        public LoginFailedException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        protected LoginFailedException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}