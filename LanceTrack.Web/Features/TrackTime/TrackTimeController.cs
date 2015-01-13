using System.Web.Mvc;

namespace LanceTrack.Web.Features.TrackTime
{
    [Authorize]
    public partial class TrackTimeController : Controller
    {
        public virtual ActionResult MyTimeTemplate()
        {
            return PartialView();
        }
    }
}