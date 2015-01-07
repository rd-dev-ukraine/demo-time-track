namespace LanceTrack.Cqrs.Contract
{
    public interface IEvent<TAggregateRoot, TAggregateRootId>
        where TAggregateRoot : class, IAggregateRoot<TAggregateRootId>
    {
        /// <summary>
        /// Gets identifier of aggregate root where event belongs to.
        /// </summary>
        TAggregateRootId AggregateRootId { get; }
    }
}