namespace EventHorizon.Game.Client.Systems.Player.State
{
    using System;
    using EventHorizon.Game.Client.Systems.Player.Api;

    public class StandardPlayerState
        : IPlayerState
    {
        public Option<IPlayerEntity> Player { get; private set; } = new Option<IPlayerEntity>(null);

        public void Set(
            IPlayerEntity player
        )
        {
            Player = new Option<IPlayerEntity>(player);
        }
    }
}
