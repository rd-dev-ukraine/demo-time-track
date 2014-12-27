using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LanceTrack.Web.Features.TrackTime
{
    public partial class TrackTimeController : Controller
    {
        // GET: TrackTime
        public virtual ActionResult Index()
        {
            return View(new object());
        }
    }
}