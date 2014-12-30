using System.Web.Mvc;

namespace LanceTrack.Web.Features.Home
{
    public partial class HomeController : Controller
    {
        public virtual ActionResult Index()
        {
            return View();
        }
    }
}