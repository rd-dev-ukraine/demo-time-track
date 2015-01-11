namespace LanceTrack.Cqrs.Contract
{
    /// <summary>
    ///     Implement a set of this interface on <see cref="IAggregateRoot{TAggregateRootId}" /> implementation
    ///     to declare support of handling <see cref="IEvent{TAggregateRoot, TAggregateRootId}" /> of particular type.
    /// </summary>
    public interface IEventRecipient<TEvent, TAggregateRoot, TAggregateRootId>
        where TAggregateRoot : class, IAggregateRoot<TAggregateRootId>
        where TEvent : IEvent<TAggregateRoot, TAggregateRootId>
    {
        void On(TEvent @event);
    }
}