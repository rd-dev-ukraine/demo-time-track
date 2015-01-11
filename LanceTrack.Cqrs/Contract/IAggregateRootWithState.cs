namespace LanceTrack.Cqrs.Contract
{
    /// <summary>
    /// Declares a type of <see cref="IAggregateRootState{TAggregateRootId}"/> state maintained by
    /// the aggregate root. 
    /// 
    /// Aggregate root state is internal to the aggregate root instance and are used 
    /// by the aggregate root to make decisions about command handling. 
    /// 
    /// 
    /// Aggregate root state must be changed due event handling.
    /// 
    /// Aggregate root state is passed to read model when it handles an event. 
    /// Usually read models and aggregate root needs the same type of data in their logic, 
    /// so sharing results avoids duplication of calculations.
    /// </summary>
    public interface IAggregateRootWithState<out TState, TAggregateRootId> : IAggregateRoot<TAggregateRootId>
        where TState: IAggregateRootState<TAggregateRootId>
    {
        /// <summary>
        /// Gets the state of the aggregate root. 
        /// </summary>
        TState State { get; }
    }
}