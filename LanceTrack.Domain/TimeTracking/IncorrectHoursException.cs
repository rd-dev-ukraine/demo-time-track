using System;
using System.Runtime.Serialization;

namespace LanceTrack.Domain.TimeTracking
{
    public class IncorrectHoursException : Exception
    {
        public IncorrectHoursException()
        {
        }

        public IncorrectHoursException(string message)
            : base(message)
        {
        }

        public IncorrectHoursException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        protected IncorrectHoursException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}