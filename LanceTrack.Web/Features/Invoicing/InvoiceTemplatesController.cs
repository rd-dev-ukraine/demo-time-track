using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LanceTrack.Web.Features.Invoicing
{
    [Authorize]
    public partial class InvoiceTemplatesController : Controller
    {
        public virtual ActionResult InvoiceListTemplate()
        {
            return PartialView(MVC.Invoicing.Views.InvoiceListTemplate);
        }

        public virtual ActionResult BillProjectTemplate()
        {
            return PartialView(MVC.Invoicing.Views.BillProjectTemplate);
        }

        public virtual ActionResult InvoiceDetailsTemplate()
        {
            return PartialView(MVC.Invoicing.Views.InvoiceDetailsTemplate);
        }
    }
}