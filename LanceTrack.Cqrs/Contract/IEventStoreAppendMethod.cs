namespace LanceTrack.Cqrs.Contract
{
    /// <summary>
    /// Must be implemented on an <see cref="IEventStore{TAggregateRoot,TAggregateRootId}"/> 
    /// implementation in order to support storing of particular type of events.
    /// </summary>
    public interface IEventStoreAppendMethod<TEvent, TAggregateRoot, TAggregateRootId>
        where TEvent : IEvent<TAggregateRoot, TAggregateRootId>
        where TAggregateRoot : class, IAggregateRoot<TAggregateRootId>
    {
        void Append(TEvent @event);
    }
}