namespace LanceTrack.Domain.Invoicing
{
    public class InvoiceRecalculationResult
    {
        public int UserId { get; set; }

        public decimal MaxHours { get; set; }

        public decimal BillingHours { get; set; }

        public decimal Sum { get; set; }
    }
}