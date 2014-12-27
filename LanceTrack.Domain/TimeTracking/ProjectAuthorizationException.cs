using System;
using System.Runtime.Serialization;

namespace LanceTrack.Domain.TimeTracking
{
    /// <summary>
    ///     User has no access to project reporting.
    /// </summary>
    public class ProjectAuthorizationException : Exception
    {
        public ProjectAuthorizationException()
        {
        }

        public ProjectAuthorizationException(string message)
            : base(message)
        {
        }

        public ProjectAuthorizationException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        protected ProjectAuthorizationException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}