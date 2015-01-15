using System.Collections.Generic;
using LanceTrack.Cqrs.Contract;
using LanceTrack.Domain.Invoicing;

namespace LanceTrack.Server.Cqrs.ProjectTime.Commands
{
    public class RecalculateInvoiceInfoCommand : ICommandWithResult<List<InvoiceRecalculationResult>, ProjectTimeAggregateRoot, int>
    {
        public int ProjectId { get; set; }

        public int ByUserId { get; set; }

        public List<InvoiceUserRequest> InvoiceUserRequest { get; set; }

        public List<InvoiceRecalculationResult> Result { get; set; }

        int ICommand<ProjectTimeAggregateRoot, int>.AggregateRootId { get { return ProjectId; } }
    }
}