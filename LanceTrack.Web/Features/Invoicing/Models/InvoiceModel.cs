using System.Collections.Generic;
using LanceTrack.Domain.Invoicing;
using LanceTrack.Domain.Projects;
using LanceTrack.Domain.UserAccounts;
using TypeLite;

namespace LanceTrack.Web.Features.Invoicing.Models
{
    [TsClass(Module = "Api")]
    public class InvoiceModel
    {
        public Invoice Invoice { get; set; }

        public List<InvoiceDetails> Details { get; set; }

        public Project Project { get; set; }

        public List<UserAccount> Users { get; set; } 
    }
}