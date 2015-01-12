using System;

namespace LanceTrack.Server.Cqrs.ProjectTime.State
{
    public class InvoiceInfo
    {
        public string Number { get; set; }

        public DateTimeOffset BilledAt { get; set; }

        public bool IsPaid { get; set; }
    }
}