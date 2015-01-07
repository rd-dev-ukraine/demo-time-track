namespace LanceTrack.Cqrs.Contract
{
    public interface ICommand<TAggregateRoot, TAggregateRootId>
        where TAggregateRoot : class, IAggregateRoot<TAggregateRootId>
    {
        TAggregateRootId AggregateRootId { get; }
    }
}