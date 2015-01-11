namespace LanceTrack.Server.Cqrs.ProjectTime.State
{
    public class UserBillingHours
    {
        public int UserId { get; set; }

        public decimal Hours { get; set; }

        public decimal Rate { get; set; }
    }
}
