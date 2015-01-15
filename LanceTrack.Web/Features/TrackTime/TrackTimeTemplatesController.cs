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

        public virtual ActionResult MyTimeTemplate()
        {
            return PartialView(MVC.TrackTime.Views.MyTimeTemplate);
        }

        public virtual ActionResult UserTimeTemplate()
        {
            return PartialView(MVC.TrackTime.Views.UsersTimeTemplate);
        }

        public virtual ActionResult TimeCellTemplate()
        {
            return PartialView(MVC.TrackTime.Views.TimeCellTemplate);
        }
    }
}