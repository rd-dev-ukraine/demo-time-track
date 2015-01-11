namespace LanceTrack.Cqrs.Contract
{
    /// <summary>
    ///     Updates read models (the R part of CQRS) for particular aggregate root instance by handling set of
    ///     <see cref="IEvent{TAggregateRoot,TAggregateRootId}" />.
    ///     Events are handled by polymorphic invokation of On(TEvent) method.
    ///     Implementation could implement a set of <see cref="IReadModelEventRecipient{TEvent,TState,TAggregateRoot,TAggregateRootId}"/>
    ///     interfaces for each type of supported <see cref="IEvent{TAggregateRoot,TAggregateRootId}"/>.
    ///     The aggregate root could contains any number of read model types (each must implement this interface).
    ///     Each instance of <see cref="IReadModelManager{TAggregateRootId}" /> could maintain any number
    ///     of read model instances of any type. But it is preferred each manager maintain only one type of read models.
    /// </summary>
    public interface IReadModelManager<TAggregateRootId>
    {
        /// <summary>
        ///     Saves changes made to read models maintained by the current instance.
        /// </summary>
        void Save();
    }
}