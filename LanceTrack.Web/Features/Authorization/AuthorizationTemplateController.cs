using System.Web.Mvc;

namespace LanceTrack.Web.Features.Authorization
{
    public partial class AuthorizationTemplateController : Controller
    {
        // GET: AuthorizationTemplate
        public virtual ActionResult LoginTemplate()
        {
            return PartialView(MVC.Authorization.Views.LoginTemplate);
        }
    }
}