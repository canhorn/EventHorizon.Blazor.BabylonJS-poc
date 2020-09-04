namespace EventHorizon.Game.Server.ServerModule.Game.Api
{
    using System;

    public interface GamePlayerScore
    {
        long PlayerEntityId { get; }
        int Score { get; }
    }
}
