namespace LanceTrack.Cqrs.Contract
{
    public interface IAggregateRootState<TAggregateRootId>
    {
        TAggregateRootId AggregateRootId { get; }
    }
}