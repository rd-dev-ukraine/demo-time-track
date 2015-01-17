using System;
using System.Diagnostics;

namespace LanceTrack.Server.Cqrs.ProjectTime.State
{
    [DebuggerDisplay("{UserId}: {At} {Hours}")]
    public class UserBilling
    {
        public int UserId { get; set; }

        public DateTime At { get; set; }

        public decimal Hours { get; set; }

        public decimal Rate { get; set; }

        public string InvoiceNum { get; set; }

        public bool IsPaid { get; set; }
    }
}
