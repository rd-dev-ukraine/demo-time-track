namespace LanceTrack.Cqrs.Contract
{
    public interface IAggregateRootWithState<out TState, TAggregateRootId> : IAggregateRoot<TAggregateRootId>
        where TState: IAggregateRootState<TAggregateRootId>
    {
        TState State { get; }
    }
}