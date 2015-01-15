using System.Collections.Generic;
using LanceTrack.Cqrs.Contract;
using LanceTrack.Domain.Invoicing;

namespace LanceTrack.Server.Cqrs.ProjectTime.Commands
{
    public class BillProjectCommand : ICommandWithResult<string, ProjectTimeAggregateRoot, int>
    {
        public int ProjectId { get; set; }

        public int ByUserId { get; set; }

        public List<InvoiceUserRequest> InvoiceUserRequest { get; set; }

        /// <summary>
        /// Gets new invoice number.
        /// </summary>
        public string Result { get; set; }

        int ICommand<ProjectTimeAggregateRoot, int>.AggregateRootId { get { return ProjectId; } }
    }
}