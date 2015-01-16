using System.Collections.Generic;
using LanceTrack.Domain.Invoicing;
using TypeLite;

namespace LanceTrack.Web.Features.Invoicing.Models
{
    [TsClass(Module = "Api")]
    public class InvoiceModel
    {
        public Invoice Invoice { get; set; }

        public List<InvoiceDetails> Details { get; set; } 
    }
}