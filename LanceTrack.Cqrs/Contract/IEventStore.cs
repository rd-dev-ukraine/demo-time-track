using System.Collections.Generic;

namespace LanceTrack.Cqrs.Contract
{
    /// <summary>
    ///  Event store provides read and write access to an events of particular aggregate root.
    ///  It is invoked internally due maintaining aggregate root lifecycle.
    ///  Event store must implement a set of <see cref="IEventStoreAppendMethod{TEvent,TAggregateRoot,TAggregateRootId}" /> 
    ///  which will be dynamically invoked on event saving.     
    /// </summary>
    public interface IEventStore<TAggregateRoot, TAggregateRootId>
        where TAggregateRoot : class, IAggregateRoot<TAggregateRootId>
    {
        /// <summary>
        ///     Reads all events for aggregate root instance of specified type.
        /// </summary>
        IEnumerable<IEvent<TAggregateRoot, TAggregateRootId>> ReadAggregateRootEvents(TAggregateRootId aggregateRootId);
    }
}