namespace EventHorizon.Game.Server.Game.Api
{
    using System;

    public interface ServerGameState
    {
        GameState GameState { get; }

        void Set(
            GameState gameState
        );
    }
}
