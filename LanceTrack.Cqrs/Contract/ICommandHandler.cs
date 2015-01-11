using System.Collections.Generic;

namespace LanceTrack.Cqrs.Contract
{
    /// <summary>
    /// Implement a set of this interface on <see cref="IAggregateRoot{TAggregateRootId}"/> implementation
    /// to declare support of handling <see cref="ICommand{TAggregateRoot, TAggregateRootId}"/> of particular type.
    /// </summary>
    public interface ICommandHandler<TCommand, TAggregateRoot, TAggregateRootId>
        where TCommand : ICommand<TAggregateRoot, TAggregateRootId>
        where TAggregateRoot : class, IAggregateRoot<TAggregateRootId>
    {
        /// <summary>
        /// Each command handled by aggregate root must fail to exception or produce a set of events.
        /// Note: events will be applied to aggregate root after command executing. 
        /// </summary>
        IEnumerable<IEvent<TAggregateRoot, TAggregateRootId>> Execute(TCommand command);
    }
}