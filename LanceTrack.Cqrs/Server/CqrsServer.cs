using System;
using System.Collections.Generic;
using System.Linq;
using LanceTrack.Cqrs.Contract;

namespace LanceTrack.Cqrs.Server
{
    public class CqrsServer : ICqrs
    {
        private readonly Dictionary<Type, IAggregateRootServer> _aggregateRootServers;
 
        public CqrsServer(IEnumerable<IAggregateRootServer> aggregateRootServers)
        {
            if (aggregateRootServers == null)
                throw new ArgumentNullException("aggregateRootServers");

            _aggregateRootServers = aggregateRootServers.ToDictionary(s => s.AggregateRootType);
        }

        public void Execute<TCommand, TAggregateRoot, TAggregateRootId>(TCommand command) 
            where TCommand : ICommand<TAggregateRoot, TAggregateRootId> 
            where TAggregateRoot : class, IAggregateRoot<TAggregateRootId>
        {
            IAggregateRootServer aggregateRootServer;

            if (!_aggregateRootServers.TryGetValue(typeof(TAggregateRoot), out aggregateRootServer))
                throw new ArgumentException("Aggregate root type is not supported.");

            ((dynamic)aggregateRootServer).Execute(command);
        }

        public void Recalculate<TAggregateRoot, TAggregateRootId>(TAggregateRootId id)
        {
            throw new System.NotImplementedException();
        }
    }
}