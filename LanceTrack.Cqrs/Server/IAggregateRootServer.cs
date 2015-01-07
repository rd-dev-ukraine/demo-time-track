using System;

namespace LanceTrack.Cqrs.Server
{
    /// <summary>
    /// Interface of aggregate root server. Used to maintain collection of aggregate root servers.
    /// </summary>
    public interface IAggregateRootServer
    {
        /// <summary>
        /// Gets a type of aggregate root.
        /// </summary>
        Type AggregateRootType { get; } 
    }
}