namespace EventHorizon.Game.Client.Systems.ClientScripts.Set;

using System;
using System.Threading;
using System.Threading.Tasks;

using EventHorizon.Observer.Model;
using EventHorizon.Observer.State;

using MediatR;

public struct ClientScriptsAssemblySetEvent : INotification { }

public interface ClientScriptsAssemblySetEventObserver
    : ArgumentObserver<ClientScriptsAssemblySetEvent> { }

public class ClientScriptsAssemblySetEventHandler
    : INotificationHandler<ClientScriptsAssemblySetEvent>
{
    private readonly ObserverState _observer;

    public ClientScriptsAssemblySetEventHandler(ObserverState observer)
    {
        _observer = observer;
    }

    public Task Handle(
        ClientScriptsAssemblySetEvent notification,
        CancellationToken cancellationToken
    ) =>
        _observer.Trigger<
            ClientScriptsAssemblySetEventObserver,
            ClientScriptsAssemblySetEvent
        >(notification, cancellationToken);
}
