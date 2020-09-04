namespace EventHorizon.Game.Server.ServerModule.Game.Model
{
    using System;
    using EventHorizon.Game.Server.ServerModule.Game.Api;

    public class GamePlayerScoreModel
        : GamePlayerScore
    {
        public long PlayerEntityId { get; set; }
        public int Score { get; set; }
    }
}
