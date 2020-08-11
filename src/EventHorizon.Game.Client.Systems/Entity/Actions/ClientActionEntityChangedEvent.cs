﻿namespace EventHorizon.Game.Client.Systems.Entity.Actions
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using EventHorizon.Game.Client.Engine.Systems.ClientAction.Api;
    using EventHorizon.Game.Client.Engine.Systems.Entity.Api;
    using EventHorizon.Observer.Model;
    using EventHorizon.Observer.State;
    using MediatR;

    // [ClientAction("EntityClientChanged")]
    public struct ClientActionEntityChangedEvent
        : INotification
    {
        public IObjectEntityDetails Details { get; }

        public ClientActionEntityChangedEvent(
            IClientActionDataResolver resolver
        )
        {
            Details = resolver.Resolve<IObjectEntityDetails>("entityId");
        }
    }

    public interface ClientActionEntityChangedEventObserver
        : ArgumentObserver<ClientActionEntityChangedEvent>
    {
    }

    public class ClientActionEntityChangedEventHandler
        : INotificationHandler<ClientActionEntityChangedEvent>
    {
        private readonly ObserverState _observer;

        public ClientActionEntityChangedEventHandler(
            ObserverState observer
        )
        {
            _observer = observer;
        }

        public Task Handle(
            ClientActionEntityChangedEvent notification,
            CancellationToken cancellationToken
        ) => _observer.Trigger<ClientActionEntityChangedEventObserver, ClientActionEntityChangedEvent>(
            notification,
            cancellationToken
        );
    }
}
