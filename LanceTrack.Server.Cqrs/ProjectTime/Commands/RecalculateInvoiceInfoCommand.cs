using LanceTrack.Cqrs.Contract;

namespace LanceTrack.Server.Cqrs.ProjectTime.Commands
{
    public class RecalculateInvoiceInfoCommand : ICommandWithResult<RecalculateInvoiceInfoCommandResult, ProjectTimeAggregateRoot, int>
    {
        public int ProjectId { get; set; }

        public int UserId { get; set; }

        public decimal BilledHours { get; set; }

        int ICommand<ProjectTimeAggregateRoot, int>.AggregateRootId { get { return ProjectId; } }

        public RecalculateInvoiceInfoCommandResult Result { get; set; }
    }
}