using System.Web.Http;
using System.Web.Http.ExceptionHandling;

using Elmah.Contrib.WebApi;

using Newtonsoft.Json.Serialization;

namespace LanceTrack.Web
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.Formatters.JsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();

            config.MapHttpAttributeRoutes();
            config.Services.Add(typeof(IExceptionLogger), new ElmahExceptionLogger());
        }
    }
}