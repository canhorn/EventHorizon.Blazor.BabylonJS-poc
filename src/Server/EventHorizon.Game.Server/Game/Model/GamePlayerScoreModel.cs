namespace EventHorizon.Game.Server.Game.Model
{
    using System;
    using EventHorizon.Game.Server.Game.Api;

    public class GamePlayerScoreModel
        : GamePlayerScore
    {
        public long PlayerEntityId { get; set; }
        public int Score { get; set; }
    }
}
