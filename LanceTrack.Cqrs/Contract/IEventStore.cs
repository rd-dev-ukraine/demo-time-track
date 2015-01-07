using System.Collections.Generic;

namespace LanceTrack.Cqrs.Contract
{
    /// <summary>
    ///     Event store must expose set of Append(TEvent) methods which will be dynamically invoked on event saving.
    ///     Implement a set of <see cref="IEventStoreAppendMethod{TEvent,TAggregateRoot,TAggregateRootId}" />
    ///     on event store implementation to declare support of saving particular event type.
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