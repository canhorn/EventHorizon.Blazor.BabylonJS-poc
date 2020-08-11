namespace EventHorizon.Game.Client.Systems.Entity.Properties.Move.Mapper
{
    using System;
    using EventHorizon.Game.Client.Core.Mapper.Api;
    using EventHorizon.Game.Client.Systems.Entity.Properties.Move.Api;
    using EventHorizon.Game.Client.Systems.Entity.Properties.Move.Model;

    public class StandardMovementStateMapper
        : IMapper<IMovementState>
    {
        public IMovementState Map(
            object obj
        ) => obj.Cast<StandardMovementState>();
    }
}
