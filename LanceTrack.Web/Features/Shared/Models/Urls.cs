using TypeLite;

namespace LanceTrack.Web.Features.Shared.Models
{
    [TsClass(Module = "Api")]
    public class Urls
    {
        public DataUrls Data { get; set; }
        public TemplatesUrls Templates { get; set; }

        [TsClass(Module = "Api")]
        public class TemplatesUrls
        {
            public string TrackMyTime { get; set; }
        }

        [TsClass(Module = "Api")]
        public class DataUrls
        {
            public string LoadProjectTime { get; set; }
            public string Recalculate { get; set; }
            public string RecalculateInvoice { get; set; }
            public string Statistics { get; set; }
            public string Track { get; set; }
        }
    }
}