namespace EventHorizon.Game.Client.Engine.Systems.Entity.Api
{
    public interface IServerTransform
    {
        IServerVector3 Position { get; }
        IServerVector3 Rotation { get; }
        IServerVector3 Scaling { get; }
        long? ScalingDeterminant { get; }
    }
}
