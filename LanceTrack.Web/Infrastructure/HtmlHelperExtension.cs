using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace LanceTrack.Web.Infrastructure
{
    public static class HtmlHelperExtension
    {
        public static IHtmlString JsonEncode(this HtmlHelper html, object data)
        {
            return html.Raw(JsonConvert.SerializeObject(data, new JsonSerializerSettings
                {
                    ContractResolver = new CamelCasePropertyNamesContractResolver()
                }));
        }
    }
}