namespace EventHorizon.Game.Client.Engine.Gui.Reload;

using System;
using System.Threading;
using System.Threading.Tasks;

using EventHorizon.Observer.Model;
using EventHorizon.Observer.State;

using MediatR;

public struct GuiSystemFinishedReloadingEvent : INotification { }

public interface GuiSystemFinishedReloadingEventObserver
    : ArgumentObserver<GuiSystemFinishedReloadingEvent> { }

public class GuiSystemFinishedReloadingEventObserverHandler
    : INotificationHandler<GuiSystemFinishedReloadingEvent>
{
    private readonly ObserverState _observer;

    public GuiSystemFinishedReloadingEventObserverHandler(
        ObserverState observer
    )
    {
        _observer = observer;
    }

    public Task Handle(
        GuiSystemFinishedReloadingEvent notification,
        CancellationToken cancellationToken
    ) =>
        _observer.Trigger<
            GuiSystemFinishedReloadingEventObserver,
            GuiSystemFinishedReloadingEvent
        >(notification, cancellationToken);
}
