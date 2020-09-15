namespace EventHorizon.Game.Server.Game.Api
{
    using System;

    public interface GamePlayerScore
    {
        long PlayerEntityId { get; }
        int Score { get; }
    }
}
