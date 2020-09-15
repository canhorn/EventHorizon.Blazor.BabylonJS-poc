namespace EventHorizon.Game.Server.Game.Model
{
    using System;
    using System.Collections.Generic;
    using EventHorizon.Game.Server.Game.Api;

    public class GameStateModel
        : GameState
    {
        public List<GamePlayerScoreModel> Scores { get; set; } = new List<GamePlayerScoreModel>();
        IEnumerable<GamePlayerScore> GameState.Scores => Scores;
    }
}
