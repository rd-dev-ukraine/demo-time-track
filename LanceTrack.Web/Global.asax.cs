using System;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;

namespace LanceTrack.Web
{
    public class Global : HttpApplication
    {
        private void Application_Start(object sender, EventArgs e)
        {
            // Code that runs on application startup
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            // Please DO NOT replace this copy-paste views pathes 
            // with variable declaration, because ReSharper can't find
            // views that way.
            ViewEngines.Engines.Clear();
            ViewEngines.Engines.Add(new RazorViewEngine
            {
                ViewLocationFormats = new[]
                {
                    "~/Features/{1}/Views/{0}.cshtml",
                    "~/Features/{1}/{0}.cshtml",
                    "~/Features/Shared/Views/{0}.cshtml",
                    "~/Features/Shared/{0}.cshtml"
                },
                MasterLocationFormats = new[]
                {
                    "~/Features/{1}/Views/{0}.cshtml",
                    "~/Features/{1}/{0}.cshtml",
                    "~/Features/Shared/Views/{0}.cshtml",
                    "~/Features/Shared/{0}.cshtml"
                },
                PartialViewLocationFormats = new[]
                {
                    "~/Features/{1}/Views/{0}.cshtml",
                    "~/Features/{1}/{0}.cshtml",
                    "~/Features/Shared/Views/{0}.cshtml",
                    "~/Features/Shared/{0}.cshtml"
                }
            });
        }
    }
}