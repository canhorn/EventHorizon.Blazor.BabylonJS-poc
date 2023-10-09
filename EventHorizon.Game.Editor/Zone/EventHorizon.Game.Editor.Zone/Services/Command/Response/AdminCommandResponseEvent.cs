namespace EventHorizon.Game.Editor.Client.Zone.Services.Command.Response;

using System;
using System.Threading;
using System.Threading.Tasks;

using EventHorizon.Game.Editor.Services.Model.Command;
using EventHorizon.Observer.Model;
using EventHorizon.Observer.State;

using MediatR;

public struct AdminCommandResponseEvent : INotification
{
    public AdminCommandResponse Response { get; }

    public AdminCommandResponseEvent(AdminCommandResponse response)
    {
        Response = response;
    }
}

public interface AdminCommandResponseEventObserver
    : ArgumentObserver<AdminCommandResponseEvent> { }

public class AdminCommandResponseEventObserverHandler
    : INotificationHandler<AdminCommandResponseEvent>
{
    private readonly ObserverState _observer;

    public AdminCommandResponseEventObserverHandler(ObserverState observer)
    {
        _observer = observer;
    }

    public Task Handle(
        AdminCommandResponseEvent notification,
        CancellationToken cancellationToken
    ) =>
        _observer.Trigger<
            AdminCommandResponseEventObserver,
            AdminCommandResponseEvent
        >(notification, cancellationToken);
}
