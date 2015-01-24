using TypeLite;

namespace LanceTrack.Web.Features.ErrorLogging
{
    [TsClass(Module = "Api")]
    public class LogJavascriptErrorParams
    {
        public string Message { get; set; }

        public string ScriptFileUrl { get; set; }

        public int LineNumber { get; set; }

        public int ColumnNumber { get; set; }
    }
}