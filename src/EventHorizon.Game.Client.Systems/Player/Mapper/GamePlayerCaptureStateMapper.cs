namespace EventHorizon.Game.Client.Systems.Player.Mapper
{
    using System;
    using EventHorizon.Game.Client.Core.Mapper.Api;
    using EventHorizon.Game.Server.ServerModule.Game.Model;

    public class GamePlayerCaptureStateMapper
        : IMapper<IGamePlayerCaptureState>
    {
        public IGamePlayerCaptureState Map(
            object obj
        ) => obj.Cast<GamePlayerCaptureState>();
    }
}
