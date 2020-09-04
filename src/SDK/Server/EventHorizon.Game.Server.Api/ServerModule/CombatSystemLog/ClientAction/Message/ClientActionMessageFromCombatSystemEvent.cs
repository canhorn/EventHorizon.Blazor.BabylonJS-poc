namespace EventHorizon.Game.Server.ServerModule.CombatSystemLog.ClientAction.Message
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using EventHorizon.Game.Client.Engine.Systems.ClientAction.Api;
    using EventHorizon.Observer.Model;
    using EventHorizon.Observer.State;
    using MediatR;

    // TODO: [ClientAction] : Finish Implementation
    //[ClientAction("MessageFromCombatSystem")]
    public struct ClientActionMessageFromCombatSystemEvent
        : INotification,
        IClientAction
    {
        public string MessageCode { get; set; }
        public string Message { get; set; }

        public ClientActionMessageFromCombatSystemEvent(
            IClientActionDataResolver resolver
        )
        {
            MessageCode = resolver.Resolve<string>(
                "messageCode"
            );
            Message = resolver.Resolve<string>(
                "message"
            );
        }
    }

    public interface ClientActionMessageFromCombatSystemEventObserver
        : ArgumentObserver<ClientActionMessageFromCombatSystemEvent>
    {
    }

    // TODO: Move this into an Implementation Project, Remove from the SDK
    public class ClientActionMessageFromCombatSystemEventHandler
        : INotificationHandler<ClientActionMessageFromCombatSystemEvent>
    {
        private readonly ObserverState _observer;

        public ClientActionMessageFromCombatSystemEventHandler(
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
