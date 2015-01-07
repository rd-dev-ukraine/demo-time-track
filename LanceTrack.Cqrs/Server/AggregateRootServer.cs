using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using LanceTrack.Cqrs.Contract;

namespace LanceTrack.Cqrs.Server
{
    /// <summary>
    ///     Basic implementation of class which manages collection of aggregate root of particular type.
    ///     It is responsible for executing commands on aggregate root and saving and applying events.
    /// </summary>
    public abstract class AggregateRootServer<TAggregateRoot, TAggregateRootId> : IAggregateRootServer
        where TAggregateRoot : class, IAggregateRoot<TAggregateRootId>
    {
        private readonly ConcurrentDictionary<TAggregateRootId, TAggregateRoot> _aggregateRoots = new ConcurrentDictionary<TAggregateRootId, TAggregateRoot>();

        protected AggregateRootServer(
            IEventStore<TAggregateRoot, TAggregateRootId> eventStore,
            Func<TAggregateRoot> aggregateRootFactory)
        {
            if (eventStore == null)
                throw new ArgumentNullException("eventStore");

            EventStore = eventStore;
            AggregateRootFactory = aggregateRootFactory;
        }

        public Type AggregateRootType
        {
            get { return typeof (TAggregateRoot); }
        }

        protected Func<TAggregateRoot> AggregateRootFactory { get; private set; }

        protected IEventStore<TAggregateRoot, TAggregateRootId> EventStore { get; private set; }

        /// <summary>
        ///     Executes command against aggregate root instance and saves and applies produced events.
        /// </summary>
        public void Execute(ICommand<TAggregateRoot, TAggregateRootId> command)
        {
            if (command == null)
                throw new ArgumentNullException("command");

            var aggregateRootInstance = _aggregateRoots.GetOrAdd(command.AggregateRootId, RestoreAggregateRoot);

            IEvent<TAggregateRoot, TAggregateRootId>[] events;

            lock (aggregateRootInstance)
            {
                // Dynamically dispatch command based on it runtime type.
                events = ((IEnumerable<IEvent<TAggregateRoot, TAggregateRootId>>) ((dynamic) aggregateRootInstance).Execute(command)).ToArray();

                // Applies event on aggregate root
                foreach (var e in events)
                    ((dynamic) aggregateRootInstance).On(e);
            }

            // Saves events in store
            foreach (var e in events)
                ((dynamic) EventStore).Append(e);
        }

        /// <summary>
        ///     Creates aggregate root instance and restores its state by reading and applying all related events.
        /// </summary>
        protected virtual TAggregateRoot RestoreAggregateRoot(TAggregateRootId id)
        {
            var aggregateRootInstance = AggregateRootFactory();

            foreach (var evnt in EventStore.ReadAggregateRootEvents(id))
                ((dynamic) aggregateRootInstance).On(evnt);

            return aggregateRootInstance;
        }
    }
}