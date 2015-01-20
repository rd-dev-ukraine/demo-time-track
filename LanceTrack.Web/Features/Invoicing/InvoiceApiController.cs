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
                return Created(Url.Link("InvoiceDetails", new { invoiceNumber = invoiceNum }), invoiceNum);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("pending", Name = "PendingInvoices"), HttpGet]
        public List<InvoiceInfo> PendingInvoices()
        {
            return _invoiceService.MyPendingInvoices().ToList();
        }

        [Route("archive", Name = "ArchiveInvoices"), HttpGet]
        public List<InvoiceInfo> ArchiveInvoices()
        {
            return _invoiceService.Archive().ToList();
        }

        [Route("details/{*invoiceNumber}", Name = "InvoiceDetails"), HttpGet]
        public IHttpActionResult Details(string invoiceNumber)
        {
            var invoice = _invoiceService.Get(invoiceNumber);
            if (invoice == null)
                return NotFound();

            var project = _projectService.BillableProject(invoice.ProjectId);
            if (project == null)
                return NotFound();


            var details = _invoiceService.Details(invoiceNumber);

            return Ok(new InvoiceModel
            {
                Project = project,
                Users = _userService.All().ToList(),
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

        [Route("distribute-earnings", Name = "DistributeInvoiceEarnings"), HttpPost]
        public IHttpActionResult DistributeInvoiceEarnings(DistributeInvoiceEarningsParam parameters)
        {
            try
            {
                _invoiceService.DistributeInvoiceEarnings(
                    parameters.ProjectId,
                    parameters.InvoiceNum,
                    parameters.EarningsSum);

                return Details(parameters.InvoiceNum);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("mark-as-paid", Name = "MarkInvoiceAsPaid"), HttpPost]
        public IHttpActionResult MarkInvoiceAsPaid(InvoiceIdParam parameters)
        {
            try
            {
                _invoiceService.MarkInvoiceAsPaid(parameters.ProjectId, parameters.InvoiceNum);

                return Details(parameters.InvoiceNum);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("cancel", Name = "CancelInvoice"), HttpPost]
        public IHttpActionResult CancelInvoice(InvoiceIdParam parameters)
        {
            try
            {
                _invoiceService.CancelInvoice(parameters.ProjectId, parameters.InvoiceNum);

                return Details(parameters.InvoiceNum);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [TsClass(Module = "Api")]
        public class PrepareInvoiceParams
        {
            public List<InvoiceUserRequest> InvoiceUserRequests { get; set; }
            public int ProjectId { get; set; }
        }

        [TsClass(Module = "Api")]
        public class DistributeInvoiceEarningsParam
        {
            public int ProjectId { get; set; }
            public string InvoiceNum { get; set; }
            public decimal EarningsSum { get; set; }
        }

        [TsClass(Module = "Api")]
        public class InvoiceIdParam
        {
            public int ProjectId { get; set; }
            public string InvoiceNum { get; set; }
        }
    }
}