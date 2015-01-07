namespace LanceTrack.Cqrs.Contract
{
    /// <summary>
    /// Specialization of <see cref="IReadModelManager{TAggregateRootId}"/> interface with aggregate root type.
    /// This interface is intended to be used in aggregate root constructor parameters.
    /// </summary>
    public interface IAggregateRootReadModelManager<TAggregateRoot, TAggregateRootId> : IReadModelManager<TAggregateRootId>
        where TAggregateRoot: class, IAggregateRoot<TAggregateRootId>
    {
    }
}