namespace EventHorizon.Game.Server.Game.ClientAction.Updated
{
    using System;
    using EventHorizon.Game.Client.Engine.Systems.ClientAction.Api;
    using EventHorizon.Game.Client.Engine.Systems.ClientAction.Attributes;
    using EventHorizon.Game.Server.Game.Api;
    using EventHorizon.Observer.Model;

    [ClientAction("CLIENT_ACTION_GAME_STATE_UPDATED")]
    public struct ClientActionGameStateUpdatedEvent
        : IClientAction
    {
        public GameState GameState { get; }

        public ClientActionGameStateUpdatedEvent(
            IClientActionDataResolver resolver
        )
        {
            GameState = resolver.Resolve<GameState>("gameState");
        }
    }

    public interface ClientActionGameStateUpdatedEventObserver
        : ArgumentObserver<ClientActionGameStateUpdatedEvent>
    {
    }
}
