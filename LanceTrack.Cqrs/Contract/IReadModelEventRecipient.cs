namespace LanceTrack.Cqrs.Contract
{
    /// <summary>
    /// Declares support of particular type of <see cref="IEvent{TAggregateRoot,TAggregateRootId}"/>
    /// on <see cref="IReadModelManager{TAggregateRootId}"/> implementation.
    /// 
    /// Additonally to <see cref="IEvent{TAggregateRoot,TAggregateRootId}"/> instance it receives a state of the 
    /// <see cref="IAggregateRootWithState{TState,TAggregateRootId}"/> instance to which read model belongs to.
    /// 
    /// Receiving state allows to avoid duplicating logic and calculations already performed by the <see cref="IAggregateRoot{TAggregateRootId}"/>
    /// due maintaining its state.
    /// </summary>
    public interface IReadModelEventRecipient<TEvent, TState, TAggregateRoot, TAggregateRootId>
        where TAggregateRoot : class, IAggregateRootWithState<TState, TAggregateRootId>
        where TEvent : IEvent<TAggregateRoot, TAggregateRootId> 
        where TState : IAggregateRootState<TAggregateRootId>
    {
        void On(TEvent @event, TState state);
    }
}