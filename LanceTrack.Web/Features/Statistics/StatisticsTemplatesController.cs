using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LanceTrack.Web.Features.Statistics
{
    public partial class StatisticsTemplatesController : Controller
    {
        // GET: StatisticsTemplates
        public virtual ActionResult UserStatisticsTemplate()
        {
            return PartialView(MVC.Statistics.Views.UserStatistics);
        }
    }
}