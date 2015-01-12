using LanceTrack.Cqrs.Contract;
using LanceTrack.Domain.Invoicing;

namespace LanceTrack.Server.Cqrs.ProjectTime.Commands
{
    public class RecalculateInvoiceInfoCommand : ICommandWithResult<InvoiceRecalculationResult, ProjectTimeAggregateRoot, int>
    {
        public int ProjectId { get; set; }

        public int UserId { get; set; }

        public decimal BilledHours { get; set; }

        int ICommand<ProjectTimeAggregateRoot, int>.AggregateRootId { get { return ProjectId; } }

        public InvoiceRecalculationResult Result { get; set; }
    }
}