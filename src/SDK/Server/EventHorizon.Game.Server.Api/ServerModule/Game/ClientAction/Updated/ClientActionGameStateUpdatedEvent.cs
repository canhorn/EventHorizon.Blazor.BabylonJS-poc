namespace EventHorizon.Game.Server.ServerModule.Game.ClientAction.Updated
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using EventHorizon.Game.Client.Engine.Systems.ClientAction.Api;
    using EventHorizon.Observer.Model;
    using EventHorizon.Observer.State;
    using MediatR;

    // TODO: [ClientAction] : Finish Implementation
    // TODO: [Game] : Finish Implementation
    //[ClientAction("CLIENT_ACTION_GAME_STATE_UPDATED")]
    public struct ClientActionGameStateUpdatedEvent
        : INotification,
        IClientAction
    {
        // public GameState GameState { get; }
        public ClientActionGameStateUpdatedEvent(
            IClientActionDataResolver _
        )
        {
        }
    }

    public interface ClientActionGameStateUpdatedEventObserver
        : ArgumentObserver<ClientActionGameStateUpdatedEvent>
    {
    }

    // TODO: Move this into an Implementation Project, Remove from the SDK
    public class ClientActionGameStateUpdatedEventHandler
        : INotificationHandler<ClientActionGameStateUpdatedEvent>
    {
        private readonly ObserverState _observer;

        public ClientActionGameStateUpdatedEventHandler(
            ObserverState observer
        )
        {
            _observer = observer;
        }

        public Task Handle(
            ClientActionGameStateUpdatedEvent notification,
            CancellationToken cancellationToken
        ) => _observer.Trigger<ClientActionGameStateUpdatedEventObserver, ClientActionGameStateUpdatedEvent>(
            notification,
            cancellationToken
        );
    }
}
