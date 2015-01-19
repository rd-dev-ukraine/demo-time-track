namespace LanceTrack.Server.Cqrs.ProjectTime.Events
{
    public enum InvoiceEventType
    {
        Billing = 1,
        EarningDistribution = 2,
        Paid = 3,
        Cancel = 4
    }
}