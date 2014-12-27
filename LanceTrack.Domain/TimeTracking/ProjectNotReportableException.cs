using System;
using System.Runtime.Serialization;

namespace LanceTrack.Domain.TimeTracking
{
    public class ProjectNotReportableException : Exception
    {
        public ProjectNotReportableException()
        {
        }

        public ProjectNotReportableException(string message)
            : base(message)
        {
        }

        public ProjectNotReportableException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        protected ProjectNotReportableException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}