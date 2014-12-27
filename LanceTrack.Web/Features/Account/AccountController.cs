using System.Web.Mvc;

namespace LanceTrack.Web.Features.Account
{
    public partial class AccountController : Controller
    {
        // GET: Account
        public virtual ActionResult Index()
        {
            return View();
        }
    }
}