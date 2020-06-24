using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using EventHorizon.Game.Client.Engine.Systems.Entity.Api;
using EventHorizon.Observer.Model;
using EventHorizon.Observer.State;
using MediatR;

namespace EventHorizon.Game.Client.Engine.Systems.Entity.Register
{
    public struct ClientEntityRegistered : INotification
    {
        public IObjectEntityDetails EntityDetails { get; }

        public ClientEntityRegistered(
            IObjectEntityDetails entityDetails
        )
        {
            EntityDetails = entityDetails;
        }
    }

    public interface ClientEntityRegisteredObserver
        : ArgumentObserver<ClientEntityRegistered>
    {
    }

    public class ClientEntityRegisteredHandler
        : INotificationHandler<ClientEntityRegistered>
    {
        private readonly ObserverState _observer;

        public ClientEntityRegisteredHandler(
            ObserverState observer
        )
        {
            _observer = observer;
        }

        public Task Handle(
            ClientEntityRegistered notification,
            CancellationToken cancellationToken
        ) => _observer.Trigger<ClientEntityRegisteredObserver, ClientEntityRegistered>(
            notification,
            cancellationToken
        );
    }
}
