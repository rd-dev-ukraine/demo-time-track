using System.Collections.Generic;
using LanceTrack.Domain.Invoicing;
using LanceTrack.Domain.Projects;
using LanceTrack.Domain.UserAccounts;
using TypeLite;

namespace LanceTrack.Web.Features.Invoicing.Models
{
    [TsClass(Module = "Api")]
    public class PrepareInvoiceModel
    {
        public List<InvoiceRecalculationResult> Invoice { get; set; }
        public Project Project { get; set; }
        public List<UserAccount> Users { get; set; }
    }
}