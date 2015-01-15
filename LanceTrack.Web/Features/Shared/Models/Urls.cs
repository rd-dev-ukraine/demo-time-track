using System.Web.Mvc;
using TypeLite;

namespace LanceTrack.Web.Features.Shared.Models
{
    [TsClass(Module = "Api")]
    public class Urls
    {
        public Urls(UrlHelper urlHelper)
        {
            Url = urlHelper;
            Data = new DataUrls(urlHelper);
            Templates = new TemplatesUrls(urlHelper);
        }

        public DataUrls Data { get; set; }
        public TemplatesUrls Templates { get; set; }
        private UrlHelper Url { get; set; }

        [TsClass(Module = "Api")]
        public class TemplatesUrls
        {
            public TemplatesUrls(UrlHelper urlHelper)
            {
                Url = urlHelper;
            }

            public string TrackTimeBase
            {
                get { return Url.Action(MVC.TrackTimeTemplates.TrackTimeTemplate()); }
            }

            public string TimeCell
            {
                get { return Url.Action(MVC.TrackTimeTemplates.TimeCellTemplate()); }
            }

            public string TrackMyTime
            {
                get { return Url.Action(MVC.TrackTimeTemplates.MyTimeTemplate()); }
            }

            public string UsersTime
            {
                get { return Url.Action(MVC.TrackTimeTemplates.UserTimeTemplate()); }
            }

            private UrlHelper Url { get; set; }
        }

        [TsClass(Module = "Api")]
        public class DataUrls
        {
            public DataUrls(UrlHelper urlHelper)
            {
                Url = urlHelper;
            }

            public string LoadProjectTime
            {
                get { return Url.HttpRouteUrl("ProjectTimeInfo", new {}); }
            }

            public string RecalculateInvoice
            {
                get { return Url.HttpRouteUrl("RecalculateInvoice", new {}); }
            }

            public string Statistics
            {
                get { return Url.HttpRouteUrl("Statistics", new {}); }
            }

            public string Track
            {
                get { return Url.HttpRouteUrl("TrackTime", new {}); }
            }

            private UrlHelper Url { get; set; }
        }
    }
}