namespace EventHorizon.Game.Client.Engine.Entity.Resolver
{
    using System;
    using EventHorizon.Game.Client.Core.ModelResolver.Api;
    using EventHorizon.Game.Client.Engine.Entity.Api;
    using EventHorizon.Game.Client.Engine.Entity.Model;

    public class Vector3ModelResolver
        : IModelResolver<IVector3>
    {
        public IVector3 Resolve(
            object details
        )
        {
            return details.Cast<Vector3Model>();
        }
    }
}
