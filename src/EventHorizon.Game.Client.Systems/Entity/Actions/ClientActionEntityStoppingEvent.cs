namespace EventHorizon.Game.Client.Systems.Entity.Actions
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using EventHorizon.Game.Client.Engine.Systems.ClientAction.Api;
    using EventHorizon.Game.Client.Engine.Systems.ClientAction.Attributes;
    using EventHorizon.Observer.Model;
    using EventHorizon.Observer.State;
    using MediatR;

    [ClientAction("ClientEntityStopping")]
    public struct ClientActionEntityStoppingEvent
        : INotification,
        IClientAction
    {
        public long EntityId { get; }

        public ClientActionEntityStoppingEvent(
            IClientActionDataResolver resolver
        )
        {
            EntityId = resolver.Resolve<long>("entityId");
        }
    }

    public interface ClientActionEntityStoppingEventObserver
        : ArgumentObserver<ClientActionEntityStoppingEvent>
    {
    }

    public class ClientActionEntityStoppingEventHandler
        : INotificationHandler<ClientActionEntityStoppingEvent>
    {
        private readonly ObserverState _observer;

        public ClientActionEntityStoppingEventHandler(
            ObserverState observer
        )
        {
            _observer = observer;
        }

        public Task Handle(
            ClientActionEntityStoppingEvent notification,
            CancellationToken cancellationToken
        ) => _observer.Trigger<ClientActionEntityStoppingEventObserver, ClientActionEntityStoppingEvent>(
            notification,
            cancellationToken
        );
    }
}
