using System;
using System.Collections.Generic;
using System.Web.Http;
using LanceTrack.Domain.Invoicing;
using LanceTrack.Domain.UserAccounts;

namespace LanceTrack.Web.Features.Invoicing
{
    [RoutePrefix("api/invoice")]
    public class InvoiceApiController : ApiController
    {
        private readonly UserAccount _currentUser;
        private readonly IInvoiceService _invoiceService;

        public InvoiceApiController(UserAccount currentUser, IInvoiceService invoiceService)
        {
            if (currentUser == null)
                throw new ArgumentNullException("currentUser");
            if (invoiceService == null)
                throw new ArgumentNullException("invoiceService");

            _currentUser = currentUser;
            _invoiceService = invoiceService;
        }

        [Route("recalculate", Name="RecalculateInvoice"), HttpPost]
        public List<InvoiceRecalculationResult> Recalculate(PrepareInvoiceParams parameters)
        {
            return _invoiceService.RecalculateInvoiceInfo(parameters.ProjectId, parameters.InvoiceUserRequests);
        }

        public class PrepareInvoiceParams
        {
            public int ProjectId { get; set; }

            public List<InvoiceUserRequest> InvoiceUserRequests { get; set; }
        }
    }
}