using System;
using System.Web;
using System.Web.Http;

using Elmah;

namespace LanceTrack.Web.Features.ErrorLogging
{
    [RoutePrefix("api/error-logging")]
    public class JavascriptErrorLoggingController : ApiController
    {

        [Route("log", Name = "LogJavascriptError"), HttpPost]
        public void LogJavascriptError(LogJavascriptErrorParams parameters)
        {
            var message = String.Format("{0} \r\n at \r\n {1}:{2}:{3}", parameters.Message, parameters.ScriptFileUrl, parameters.LineNumber, parameters.ColumnNumber);

            var exception = new JavascriptException(message);

            ErrorLog.GetDefault(HttpContext.Current).Log(new Error(exception));
        }
    }
}
