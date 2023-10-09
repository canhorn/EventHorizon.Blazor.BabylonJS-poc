namespace EventHorizon.Game.Client.Engine.Gui.ClientAction;

using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

using EventHorizon.Game.Client.Engine.Gui.Api;
using EventHorizon.Game.Client.Engine.Gui.Model;
using EventHorizon.Game.Client.Engine.Systems.ClientAction.Api;
using EventHorizon.Game.Client.Engine.Systems.ClientAction.Attributes;
using EventHorizon.Observer.Model;
using EventHorizon.Observer.State;

using MediatR;

[ClientAction("GUI_SYSTEM_RELOADED_CLIENT_ACTION_EVENT")]
public struct ClientActionGuiSystemReloadedEvent : IClientAction
{
    public IEnumerable<IGuiLayoutData> GuiLayoutList { get; }

    public ClientActionGuiSystemReloadedEvent(
        IClientActionDataResolver resolver
    )
    {
        GuiLayoutList = resolver.Resolve<List<GuiLayoutDataModel>>(
            "guiLayoutList"
        );
    }
}

public interface ClientActionGuiSystemReloadedEventObserver
    : ArgumentObserver<ClientActionGuiSystemReloadedEvent> { }

public class ClientActionGuiSystemReloadedEventObserverHandler
    : INotificationHandler<ClientActionGuiSystemReloadedEvent>
{
    private readonly ObserverState _observer;

    public ClientActionGuiSystemReloadedEventObserverHandler(
        ObserverState observer
    )
    {
        _observer = observer;
    }

    public Task Handle(
        ClientActionGuiSystemReloadedEvent notification,
        CancellationToken cancellationToken
    ) =>
        _observer.Trigger<
            ClientActionGuiSystemReloadedEventObserver,
            ClientActionGuiSystemReloadedEvent
        >(notification, cancellationToken);
}
