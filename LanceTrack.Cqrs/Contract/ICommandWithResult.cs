namespace LanceTrack.Cqrs.Contract
{
    /// <summary>
    /// Some commands could return result to caller. 
    /// Such command may not produce events. 
    /// Aggregate root could respond to queries from client in this way 
    /// to avoid using read models in business logic decisions.
    /// 
    /// Commands with results are handled as regular commands but 
    /// aggregate root must assign an value to <see cref="ICommandWithResult{TResult,TAggregateRoot,TAggregateRootId}.Result"/> property.
    /// </summary>
    public interface ICommandWithResult<TResult, TAggregateRoot, TAggregateRootId> : ICommand<TAggregateRoot, TAggregateRootId> 
        where TAggregateRoot : class, IAggregateRoot<TAggregateRootId>
    {
        /// <summary>
        /// Gets or sets a result of the command execution.
        /// </summary>
        TResult Result { get; set; }
    }
}