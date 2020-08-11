namespace EventHorizon.Game.Client.Engine.Entity.Builder
{
    using EventHorizon.Game.Client.Core.Builder.Api;
    using EventHorizon.Game.Client.Engine.Entity.Api;
    using EventHorizon.Game.Client.Engine.Entity.Model;
    using EventHorizon.Game.Client.Engine.Systems.Entity.Api;

    public class StandardVector3Builder
        : IBuilder<IVector3, IServerVector3>
    {
        public IVector3 Build(
            IServerVector3 details
        )
        {
            return new StandardVector3(
                details.X,
                details.Y,
                details.Z
            );
        }
    }
}
