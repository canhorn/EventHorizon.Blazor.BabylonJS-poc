namespace EventHorizon.Game.Server.Game.Api
{
    using System;
    using System.Collections.Generic;

    public interface GameState
    {
        IEnumerable<GamePlayerScore> Scores { get; }
    }
}
