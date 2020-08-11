namespace EventHorizon.Game.Client.Systems.Entity.Actions
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using EventHorizon.Game.Client.Engine.Entity.Api;
    using EventHorizon.Game.Client.Engine.Systems.ClientAction.Api;
    using EventHorizon.Game.Client.Engine.Systems.ClientAction.Attributes;
    using EventHorizon.Observer.Model;
    using EventHorizon.Observer.State;
    using MediatR;

    [ClientAction("EntityClientMove")]
    public class ClientActionEntityMoveEvent
        : INotification,
        IClientAction
    {
        public long EntityId { get; }
        public IVector3 MoveTo { get; }

        public ClientActionEntityMoveEvent(
            IClientActionDataResolver resolver
        )
        {
            EntityId = resolver.Resolve<long>("entityId");
            MoveTo = resolver.Resolve<IVector3>("moveTo");
        }
    }

    public interface ClientActionEntityMoveEventObserver
        : ArgumentObserver<ClientActionEntityMoveEvent>
    {
    }

    public class ClientActionEntityMoveEventHandler
        : INotificationHandler<ClientActionEntityMoveEvent>
    {
        private readonly ObserverState _observer;

        public ClientActionEntityMoveEventHandler(
            ObserverState observer
        )
        {
            _observer = observer;
        }

        public Task Handle(
            ClientActionEntityMoveEvent notification,
            CancellationToken cancellationToken
        ) => _observer.Trigger<ClientActionEntityMoveEventObserver, ClientActionEntityMoveEvent>(
            notification,
            cancellationToken
        );
    }
}
