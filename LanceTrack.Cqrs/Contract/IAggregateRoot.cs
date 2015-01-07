using System.Collections.Generic;

namespace LanceTrack.Cqrs.Contract
{
    /// <summary>
    /// Aggregate root contract.
    /// Each class implements aggregate root must expose set of Execute(ICommand) and On(IEvent) methods 
    /// which will be called dynamically.
    /// </summary>
    public interface IAggregateRoot<TAggregateRootId>
    {
        /// <summary>
        /// Gets a collection of read model managers.
        /// </summary>
        IEnumerable<IReadModelManager<TAggregateRootId>> ReadModels { get; }
    }
}