namespace EventHorizon.Game.Client.Systems.Combat.Skill.ClientAction;

using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

using EventHorizon.Game.Client.Engine.Systems.ClientAction.Api;
using EventHorizon.Game.Client.Engine.Systems.ClientAction.Attributes;
using EventHorizon.Observer.Model;
using EventHorizon.Observer.State;

using MediatR;

[ClientAction("RunSkillAction")]
public struct ClientActionRunSkillActionEvent : IClientAction
{
    public string Action { get; }
    public IDictionary<string, object> Data { get; }

    public ClientActionRunSkillActionEvent(IClientActionDataResolver resolver)
    {
        Action = resolver.Resolve<string>("action");
        Data = resolver.Resolve<Dictionary<string, object>>("data");
    }
}

public interface ClientActionRunSkillActionEventObserver
    : ArgumentObserver<ClientActionRunSkillActionEvent> { }

public class ClientActionRunSkillActionEventObserverHandler
    : INotificationHandler<ClientActionRunSkillActionEvent>
{
    private readonly ObserverState _observer;

    public ClientActionRunSkillActionEventObserverHandler(
        ObserverState observer
    )
    {
        _observer = observer;
    }

    public Task Handle(
        ClientActionRunSkillActionEvent notification,
        CancellationToken cancellationToken
    ) =>
        _observer.Trigger<
            ClientActionRunSkillActionEventObserver,
            ClientActionRunSkillActionEvent
        >(notification, cancellationToken);
}
