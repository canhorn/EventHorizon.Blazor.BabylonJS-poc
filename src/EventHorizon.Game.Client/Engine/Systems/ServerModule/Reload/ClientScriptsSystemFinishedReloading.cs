using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using EventHorizon.Observer.Model;
using EventHorizon.Observer.State;
using MediatR;

namespace EventHorizon.Game.Client.Engine.Systems.ServerModule.Reload
{
    public struct ClientScriptsSystemFinishedReloading : INotification
    {

    }

    public interface ClientScriptsSystemFinsihedReloadingObserver
        : ArgumentObserver<ClientScriptsSystemFinishedReloading>
    {
    }

    public class ClientScriptsSystemFinsihedReloadingHandler
        : INotificationHandler<ClientScriptsSystemFinishedReloading>
    {
        private readonly ObserverState _observer;

        public ClientScriptsSystemFinsihedReloadingHandler(
            ObserverState observer
        )
        {
            _observer = observer;
        }

        public Task Handle(
            ClientScriptsSystemFinishedReloading notification,
            CancellationToken cancellationToken
        ) => _observer.Trigger<ClientScriptsSystemFinsihedReloadingObserver, ClientScriptsSystemFinishedReloading>(
            notification,
            cancellationToken
        );
    }
}
