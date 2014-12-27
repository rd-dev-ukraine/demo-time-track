using System;
using System.Runtime.Serialization;

namespace LanceTrack.Domain.TimeTracking
{
    public class InvalidHoursException : Exception
    {
        public InvalidHoursException()
        {
        }

        public InvalidHoursException(string message)
            : base(message)
        {
        }

        public InvalidHoursException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        protected InvalidHoursException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}