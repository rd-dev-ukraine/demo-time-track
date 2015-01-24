using System;
using System.Runtime.Serialization;
namespace LanceTrack.Web.Features.ErrorLogging
{
    public class JavascriptException : Exception
    {
        public JavascriptException()
        {
        }

        public JavascriptException(string message)
            : base(message)
        {
        }

        public JavascriptException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        protected JavascriptException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}