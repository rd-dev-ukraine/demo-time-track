namespace LanceTrack.Cqrs.Contract
{
    /// <summary>
    /// Command executed against aggregate root. Implementation will contains additional command parameters.
    /// </summary>
    public interface ICommand<TAggregateRoot, TAggregateRootId>
        where TAggregateRoot : class, IAggregateRoot<TAggregateRootId>
    {
        /// <summary>
        /// Gets the identifier of the aggregate root.
        /// </summary>
        TAggregateRootId AggregateRootId { get; }
    }
}