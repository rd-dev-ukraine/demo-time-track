namespace LanceTrack.Cqrs.Contract
{
    public interface ICqrs
    {
        /// <summary>
        ///     Executes command against aggregate root instance of specified type.
        /// </summary>
        void Execute<TCommand, TAggregateRoot, TAggregateRootId>(TCommand command)
            where TAggregateRoot : class, IAggregateRoot<TAggregateRootId>
            where TCommand : ICommand<TAggregateRoot, TAggregateRootId>;

        /// <summary>
        ///     Recalculates all read models of aggregate root.
        /// </summary>
        void Recalculate<TAggregateRoot, TAggregateRootId>(TAggregateRootId id);
    }
}