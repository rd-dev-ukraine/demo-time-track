using System.Web.Mvc;

namespace LanceTrack.Web.Features.TrackTime
{
    [Authorize]
    public partial class TrackTimeTemplatesController : Controller
    {
        public virtual ActionResult TrackTimeTemplate()
        {
            return PartialView(MVC.TrackTime.Views.TrackTimeTemplate);
        }
    }
}