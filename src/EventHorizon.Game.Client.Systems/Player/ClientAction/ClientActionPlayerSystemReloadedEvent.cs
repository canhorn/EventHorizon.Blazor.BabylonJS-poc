namespace EventHorizon.Game.Client.Systems.Player.ClientAction;

using EventHorizon.Game.Client.Engine.Systems.ClientAction.Api;
using EventHorizon.Game.Client.Engine.Systems.ClientAction.Attributes;
using EventHorizon.Game.Client.Engine.Systems.Entity.Model;
using EventHorizon.Observer.Model;

[ClientAction("Player.PLAYER_SYSTEM_RELOADED")]
public struct ClientActionPlayerSystemReloadedEvent : IClientAction
{
    public ObjectEntityConfigurationModel PlayerConfiguration { get; }

    public ClientActionPlayerSystemReloadedEvent(IClientActionDataResolver resolver)
    {
        PlayerConfiguration = resolver.Resolve<ObjectEntityConfigurationModel>(
            "playerConfiguration"
        );
    }
}

public interface ClientActionPlayerSystemReloadedEventObserver
    : ArgumentObserver<ClientActionPlayerSystemReloadedEvent> { }
