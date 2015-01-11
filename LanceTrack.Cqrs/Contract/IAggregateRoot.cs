using System.Collections.Generic;

namespace LanceTrack.Cqrs.Contract
{
    /// <summary>
    /// Aggregate root is business logic storage. 
    /// From one side it handles the client command and produces sequence of events. 
    /// From other side it handles events and maintain internal state <see cref="IAggregateRootState{TAggregateRootId}"/> 
    /// which is used to make decisions on command handling.
    /// 
    /// Implementation of <see cref="IAggregateRoot{TAggregateRootId}"/> must implements 
    /// a set of <see cref="ICommandHandler{TCommand,TAggregateRoot,TAggregateRootId}"/> for each supported command.
    /// 
    /// Implementation of <see cref="IAggregateRoot{TAggregateRootId}"/> must implements 
    /// a set of <see cref="IEventRecipient{TEvent,TAggregateRoot,TAggregateRootId}"/> interface for 
    /// each type of supported <see cref="IEvent{TAggregateRoot,TAggregateRootId}"/>.
    /// </summary>
    public interface IAggregateRoot<TAggregateRootId>
    {
        /// <summary>
        /// Gets a collection of read model managers.
        /// </summary>
        IEnumerable<IReadModelManager<TAggregateRootId>> ReadModels { get; }
    }
}