using System;

namespace LanceTrack.Cqrs.Contract
{
    /// <summary>
    /// Single domain knowlege fact expressed as element of append-only sequence.
    /// Events are produced by the aggregate root on executing command.
    /// </summary>
    public interface IEvent<TAggregateRoot, TAggregateRootId>
        where TAggregateRoot : class, IAggregateRoot<TAggregateRootId>
    {
        /// <summary>
        /// Gets identifier of aggregate root where event belongs to.
        /// </summary>
        TAggregateRootId AggregateRootId { get; }

        int Id { get; }
    }
}