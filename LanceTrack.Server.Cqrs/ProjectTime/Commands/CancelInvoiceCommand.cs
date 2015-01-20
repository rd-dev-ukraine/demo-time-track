using LanceTrack.Cqrs.Contract;

namespace LanceTrack.Server.Cqrs.ProjectTime.Commands
{
    public class CancelInvoiceCommand : ICommand<ProjectTimeAggregateRoot, int>
    {
        public int ByUserId { get; set; }

        public string InvoiceNum { get; set; }

        public int ProjectId { get; set; }

        int ICommand<ProjectTimeAggregateRoot, int>.AggregateRootId { get { return ProjectId; } }
    }
}