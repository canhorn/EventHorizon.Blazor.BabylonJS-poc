namespace EventHorizon.Game.Client.Engine.Entity.Builder
{
    using EventHorizon.Game.Client.Core.Builder.Api;
    using EventHorizon.Game.Client.Engine.Entity.Api;
    using EventHorizon.Game.Client.Engine.Entity.Model;
    using EventHorizon.Game.Client.Engine.Systems.Entity.Api;

    public class TransformBuilder
        : IBuilder<ITransform, IServerTransform>
    {
        IBuilder<IVector3, IServerVector3> _vector3Builder;

        public TransformBuilder(
            IBuilder<IVector3, IServerVector3> vector3Builder
        )
        {
            _vector3Builder = vector3Builder;
        }

        public ITransform Build(
            IServerTransform details
        )
        {
            return new TransformModel(
                _vector3Builder.Build(
                    details.Position
                ),
                _vector3Builder.Build(
                    details.Rotation
                ),
                _vector3Builder.Build(
                    details.Scaling
                ),
                details.ScalingDeterminant
            );
        }
    }
}
