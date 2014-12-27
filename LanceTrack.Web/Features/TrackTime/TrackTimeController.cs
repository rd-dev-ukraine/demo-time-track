using System.Web.Mvc;

namespace LanceTrack.Web.Features.TrackTime
{
    [Authorize]
    public partial class TrackTimeController : Controller
    {
        // GET: TrackTime
        public virtual ActionResult Index()
        {
            return View(new object());
        }
    }
}