using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using LanceTrack.Domain.Invoicing;
using LanceTrack.Domain.Projects;
using LanceTrack.Domain.UserAccounts;
using LanceTrack.Web.Features.Invoicing.Models;
using TypeLite;

namespace LanceTrack.Web.Features.Invoicing
{
    [RoutePrefix("api/invoice"), Authorize]
    public class InvoiceApiController : ApiController
    {
        private readonly UserAccount _currentUser;
        private readonly IInvoiceService _invoiceService;
        private readonly IProjectService _projectService;
        private readonly IUserService _userService;

        public InvoiceApiController(
            UserAccount currentUser,
            IInvoiceService invoiceService,
            IProjectService projectService,
            IUserService userService)
        {
            if (currentUser == null)
                throw new ArgumentNullException("currentUser");
            if (invoiceService == null)
                throw new ArgumentNullException("invoiceService");
            if (projectService == null)
                throw new ArgumentNullException("projectService");
            if (userService == null)
                throw new ArgumentNullException("userService");

            _currentUser = currentUser;
            _invoiceService = invoiceService;
            _projectService = projectService;
            _userService = userService;
        }

        [Route("bill", Name = "Bill"), HttpPost]
        public IHttpActionResult Bill(PrepareInvoiceParams parameters)
        {
            try
            {
                var invoiceNum = _invoiceService.BillProject(parameters.ProjectId, parameters.InvoiceUserRequests);
                return Created(Url.Link("InvoiceDetails", new {invoiceNumber = invoiceNum}), invoiceNum);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("details/{invoiceNumber}", Name = "InvoiceDetails")]
        public IHttpActionResult Details(string invoiceNumber)
        {
            var invoice = _invoiceService.Get(invoiceNumber);
            if (invoice == null)
                return NotFound();

            var details = _invoiceService.Details(invoiceNumber);

            return Ok(new InvoiceModel
            {
                Invoice = invoice,
                Details = details
            });
        }

        [Route("prepare/{projectId?}", Name = "PrepareInvoice"), HttpGet]
        public IHttpActionResult PrepareInvoice(int projectId)
        {
            var project = _projectService.BillableProject(projectId);
            if (project == null)
                return BadRequest("Project is not billable.");

            var users = _userService.All();

            var result = new PrepareInvoiceModel
            {
                Project = project,
                Users = users.ToList(),
                Invoice = _invoiceService.RecalculateInvoiceInfo(projectId, new List<InvoiceUserRequest>())
            };

            return Ok(result);
        }

        [Route("recalculate", Name = "RecalculateInvoice"), HttpPost]
        public List<InvoiceRecalculationResult> Recalculate(PrepareInvoiceParams parameters)
        {
            return _invoiceService.RecalculateInvoiceInfo(parameters.ProjectId, parameters.InvoiceUserRequests);
        }

        [TsClass(Module = "Api")]
        public class PrepareInvoiceParams
        {
            public List<InvoiceUserRequest> InvoiceUserRequests { get; set; }
            public int ProjectId { get; set; }
        }
    }
}