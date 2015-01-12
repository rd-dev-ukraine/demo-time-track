using LanceTrack.Cqrs.Contract;

namespace LanceTrack.Server.Cqrs.ProjectTime.Commands
{
    public class BillProjectCommand : ICommandWithResult<string, ProjectTimeAggregateRoot, int>
    {
        public int ProjectId { get; set; }

        public int UserId { get; set; }

        public decimal Hours { get; set; }

        int ICommand<ProjectTimeAggregateRoot, int>.AggregateRootId { get { return ProjectId; } }

        /// <summary>
        /// Gets new invoice number.
        /// </summary>
        public string Result { get; set; }
    }
}