namespace EventHorizon.Game.Server.Game.State
{
    using System;
    using EventHorizon.Game.Server.Game.Api;
    using EventHorizon.Game.Server.Game.Model;

    public class StandardServerGameState
        : ServerGameState
    {
        public GameState GameState { get; private set; } = new GameStateModel();

        public void Set(
            GameState gameState
        )
        {
            if (gameState.IsNotNull())
            {
                GameState = gameState;
            }
        }
    }
}
