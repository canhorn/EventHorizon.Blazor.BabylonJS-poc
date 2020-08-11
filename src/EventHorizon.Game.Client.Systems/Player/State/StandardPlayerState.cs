namespace EventHorizon.Game.Client.Systems.Player.State
{
    using System;
    using EventHorizon.Game.Client.Systems.Player.Api;

    public class StandardPlayerState
        : IPlayerState
    {
        public Option<IPlayerEntity> Player { get; private set; } = OptionFactory.Build<IPlayerEntity>(null);

        public void Set(
            IPlayerEntity player
        )
        {
            Player = OptionFactory.Build(player);
        }
    }
}
