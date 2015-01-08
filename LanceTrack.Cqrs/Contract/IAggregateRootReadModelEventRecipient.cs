namespace LanceTrack.Cqrs.Contract
{
    public interface IAggregateRootReadModelEventRecipient<TEvent, TState, TAggregateRoot, TAggregateRootId>
        where TAggregateRoot : class, IAggregateRootWithState<TState, TAggregateRootId>
        where TEvent : IEvent<TAggregateRoot, TAggregateRootId> 
        where TState : IAggregateRootState<TAggregateRootId>
    {
        void On(TEvent @event, TState state);
    }
}