using LanceTrack.Cqrs.Contract;

namespace LanceTrack.Server.Cqrs.ProjectTime.Commands
{
    public class BillProjectCommand : ICommand<ProjectTimeAggregateRoot, int>
    {
        public int ProjectId { get; set; }

        public int UserId { get; set; }

        public decimal Hours { get; set; }

        public decimal Sum { get; set; }

        public string InvoiceNum { get; set; }

        int ICommand<ProjectTimeAggregateRoot, int>.AggregateRootId { get { return ProjectId; } }
    }
}