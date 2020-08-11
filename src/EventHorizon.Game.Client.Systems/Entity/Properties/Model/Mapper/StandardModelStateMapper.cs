namespace EventHorizon.Game.Client.Systems.Entity.Properties.Model.Mapper
{
    using System;
    using EventHorizon.Game.Client.Core.Mapper.Api;
    using EventHorizon.Game.Client.Systems.Entity.Properties.Model.Api;
    using EventHorizon.Game.Client.Systems.Entity.Properties.Model.Model;

    public class StandardModelStateMapper
        : IMapper<IModelState>
    {
        public IModelState Map(
            object obj
        ) => obj.Cast<StandardModelState>();
    }
}
