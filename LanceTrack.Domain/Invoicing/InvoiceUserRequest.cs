namespace LanceTrack.Domain.Invoicing
{
    public class InvoiceUserRequest
    {
        public int UserId { get; set; }

        public decimal Hours { get; set; }
    }
}