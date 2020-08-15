namespace EventHorizon.Game.Client.Engine.Systems.Entity.Resolver
{
    using System;
    using EventHorizon.Game.Client.Core.ModelResolver.Api;
    using EventHorizon.Game.Client.Engine.Systems.Entity.Api;
    using EventHorizon.Game.Client.Engine.Systems.Entity.Model;

    public class ObjectEntityDetailsModelResolver
        : IModelResolver<IObjectEntityDetails>
    {
        public IObjectEntityDetails Resolve(
            object details
        )
        {
            return details.Cast<ObjectEntityDetailsModel>();
        }
    }
}
