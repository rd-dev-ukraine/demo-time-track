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
    public abstract class AggregateRootServer<TAggregateRoot, TState, TAggregateRootId> : IAggregateRootServer
        where TAggregateRoot : class, IAggregateRootWithState<TState, TAggregateRootId>
        where TState : IAggregateRootState<TAggregateRootId>
    {
        private readonly ConcurrentDictionary<TAggregateRootId, TAggregateRoot> _aggregateRoots = new ConcurrentDictionary<TAggregateRootId, TAggregateRoot>();
        private readonly dynamic _self;

        protected AggregateRootServer(
            IEventStore<TAggregateRoot, TAggregateRootId> eventStore,
            Func<TAggregateRoot> aggregateRootFactory)
        {
            if (eventStore == null)
                throw new ArgumentNullException("eventStore");

            EventStore = eventStore;
            AggregateRootFactory = aggregateRootFactory;
            _self = this;
        }

        public Type AggregateRootType
        {
            get { return typeof(TAggregateRoot); }
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

            // Aggregate root is locking point.
            // Command execution and event applying are performed in the lock.
            // Also read model updated inside lock to prevent state mutating due read model updating.
            lock (aggregateRootInstance)
            {
                // Dynamically dispatch command based on it runtime type.
                events = ((IEnumerable<IEvent<TAggregateRoot, TAggregateRootId>>)_self.DispatchCommandOnAggregateRoot(aggregateRootInstance, (dynamic)command)).ToArray();

                if (events.Any())
                {
                    // Applies event on aggregate root
                    foreach (var e in events)
                        _self.DispatchEventOnAggregateRoot(aggregateRootInstance, (dynamic)e);

                    // Update read models
                    foreach (var readModel in aggregateRootInstance.ReadModels)
                        lock (readModel)
                        {
                            var rm = (dynamic)readModel;
                            foreach (var e in events)
                                _self.DispatchEventOnReadModel(aggregateRootInstance, (dynamic)e, rm);
                        }
                }
            }

            if (events.Any())
            {
                // Saves events in store
                foreach (var e in events)
                    _self.AppendEventToStore((dynamic)e);

                // Update read models
                foreach (var readModel in aggregateRootInstance.ReadModels)
                    lock (readModel)
                        readModel.Save();
            }
        }

        /// <summary>
        ///     Creates aggregate root instance and restores its state by reading and applying all related events.
        /// </summary>
        protected virtual TAggregateRoot RestoreAggregateRoot(TAggregateRootId id)
        {
            var aggregateRootInstance = AggregateRootFactory();

            lock (aggregateRootInstance)
            {
                foreach (var evnt in EventStore.ReadAggregateRootEvents(id))
                {
                    _self.DispatchEventOnAggregateRoot(aggregateRootInstance, (dynamic)evnt);

                    foreach (var readModel in aggregateRootInstance.ReadModels)
                        lock (readModel)
                            _self.DispatchEventOnReadModel(aggregateRootInstance, (dynamic)evnt, (dynamic)readModel);
                }
            }

            return aggregateRootInstance;
        }

        protected virtual IEnumerable<IEvent<TAggregateRoot, TAggregateRootId>> DispatchCommandOnAggregateRoot<TCommand>(TAggregateRoot aggregateRoot, TCommand command)
            where TCommand : ICommand<TAggregateRoot, TAggregateRootId>
        {
            var commandHandler = aggregateRoot as ICommandHandler<TCommand, TAggregateRoot, TAggregateRootId>;
            if (commandHandler == null)
                throw new InvalidOperationException(String.Format("Aggregate root {0} does not support dispatching of {1} command.", typeof(TAggregateRoot), typeof(TCommand)));
            return commandHandler.Execute(command);
        }

        protected virtual void DispatchEventOnAggregateRoot<TEvent>(TAggregateRoot aggregateRoot, TEvent @event)
            where TEvent : IEvent<TAggregateRoot, TAggregateRootId>
        {
            var eventRecipient = aggregateRoot as IEventRecipient<TEvent, TAggregateRoot, TAggregateRootId>;
            if (eventRecipient == null)
                throw new InvalidOperationException(String.Format("Aggregate root {0} does not support dispatching of {1} event.", typeof(TAggregateRoot), typeof(TEvent)));


            eventRecipient.On(@event);
        }

        protected virtual void DispatchEventOnReadModel<TEvent, TReadModel>(TAggregateRoot aggregateRoot, TEvent @event, TReadModel readModel)
            where TEvent : IEvent<TAggregateRoot, TAggregateRootId>
            where TReadModel : IAggregateRootReadModelManager<TAggregateRoot, TAggregateRootId>
        {
            var eventWithStateRecipient = readModel as IReadModelEventRecipient<TEvent, TState, TAggregateRoot, TAggregateRootId>;

            if (eventWithStateRecipient == null)
                throw new InvalidOperationException(String.Format("Read model {0} does not support dispatching of {1} event.", typeof(TReadModel), typeof(TEvent)));

            eventWithStateRecipient.On(@event, aggregateRoot.State);
        }

        protected virtual void AppendEventToStore<TEvent>(TEvent @event)
            where TEvent : IEvent<TAggregateRoot, TAggregateRootId>
        {
            var appendMethod = EventStore as IEventStoreAppendMethod<TEvent, TAggregateRoot, TAggregateRootId>;
            if (appendMethod == null)
                throw new InvalidOperationException(String.Format("Event store for aggregate root {0} does not support storing of {1} event.", typeof(TAggregateRoot), typeof(TEvent)));

            appendMethod.Append(@event);
        }
    }
}