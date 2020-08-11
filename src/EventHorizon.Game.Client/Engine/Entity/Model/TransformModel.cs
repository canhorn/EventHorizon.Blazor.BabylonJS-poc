namespace EventHorizon.Game.Client.Engine.Entity.Model
{
    using EventHorizon.Game.Client.Engine.Entity.Api;

    public class TransformModel
        : ITransform
    {
        public IVector3 Position { get; }
        public IVector3 Rotation { get; }
        public IVector3 Scaling { get; }
        public decimal? ScalingDeterminant { get; }

        public TransformModel(
            IVector3 position,
            IVector3 rotation,
            IVector3 scaling,
            decimal? scalingDeterminant
        )
        {
            Position = position;
            Rotation = rotation;
            Scaling = scaling;
            ScalingDeterminant = scalingDeterminant;
        }
    }
}
