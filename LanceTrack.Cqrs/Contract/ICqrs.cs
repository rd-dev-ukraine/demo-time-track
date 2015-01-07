namespace LanceTrack.Cqrs.Contract
{
    public interface ICqrs
    {
        /// <summary>
        ///     Executes command against aggregate root instance of specified type.
        /// </summary>
        void Execute<TAggregateRoot, TAggregateRootId>(ICommand<TAggregateRoot, TAggregateRootId> command)
            where TAggregateRoot : class, IAggregateRoot<TAggregateRootId>;

        /// <summary>
        ///     Recalculates all read models of aggregate root.
        /// </summary>
        void Recalculate<TAggregateRoot, TAggregateRootId>(TAggregateRootId id);
    }
}