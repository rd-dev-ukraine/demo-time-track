using System;

namespace LanceTrack.Server.Cqrs.ProjectTime.State
{
    public class UserInvoiceInfo
    {
        public DateTime At { get; set; }
        public decimal Hours { get; set; }
        public string InvoiceNum { get; set; }
        public bool IsPaid { get; set; }
        public int UserId { get; set; }
    }
}