namespace EventHorizon.Game.Client.Systems.Player.Api
{
    using System;

    public interface IPlayerState
    {
        Option<IPlayerEntity> Player { get; }

        void Reset();
        public void Set(
            IPlayerEntity player
        );
    }
}
