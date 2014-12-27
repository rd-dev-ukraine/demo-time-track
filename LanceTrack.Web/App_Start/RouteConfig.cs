using System.Web.Mvc;
using System.Web.Routing;

namespace LanceTrack.Web
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                "Default", 
                "{controller}/{action}/{id}", 
                new 
                {
                    controller = MVC.TrackTime.Name, 
                    action = MVC.TrackTime.ActionNames.Index, 
                    id = UrlParameter.Optional
                });
        }
    }
}