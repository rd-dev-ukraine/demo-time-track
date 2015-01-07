namespace LanceTrack.Cqrs.Contract
{
    /// <summary>
    ///     Declare set of this interfaces on <see cref="IEventStore{TAggregateRoot, TAggregateRootId}" />
    ///     to declare support of saving message of particular type.
    /// </summary>
    public interface IEventStoreAppendMethod<TEvent, TAggregateRoot, TAggregateRootId>
        where TEvent : IEvent<TAggregateRoot, TAggregateRootId>
        where TAggregateRoot : class, IAggregateRoot<TAggregateRootId>
    {
        void Append(TEvent @event);
    }
}