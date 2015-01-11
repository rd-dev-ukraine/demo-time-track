namespace LanceTrack.Server.Cqrs.ProjectTime.Commands
{
    public class RecalculateInvoiceInfoCommandResult
    {
        public int ProjectId { get; set; }

        public decimal MaxBillableHours { get; set; }

        public decimal BilledHours { get; set; }

        public decimal InvoiceSum { get; set; }
    }
}