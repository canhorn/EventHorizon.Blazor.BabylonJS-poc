namespace EventHorizon.Game.Server.ServerModule.CombatSystemLog.ClientAction.Message
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using EventHorizon.Observer.State;
    using MediatR;

    public class ClientActionMessageFromCombatSystemEventObserverHandler
        : INotificationHandler<ClientActionMessageFromCombatSystemEvent>
    {
        private readonly ObserverState _observer;

        public ClientActionMessageFromCombatSystemEventObserverHandler(
            ObserverState observer
        )
        {
            _observer = observer;
        }

        public Task Handle(
            ClientActionMessageFromCombatSystemEvent notification,
            CancellationToken cancellationToken
        ) => _observer.Trigger<ClientActionMessageFromCombatSystemEventObserver, ClientActionMessageFromCombatSystemEvent>(
            notification,
            cancellationToken
        );
    }
}
