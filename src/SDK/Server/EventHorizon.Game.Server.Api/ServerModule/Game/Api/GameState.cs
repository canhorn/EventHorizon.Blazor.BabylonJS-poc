namespace EventHorizon.Game.Server.ServerModule.Game.Api
{
    using System;
    using System.Collections.Generic;

    public interface GameState
    {
        IEnumerable<GamePlayerScore> Scores { get; }
    }
}
