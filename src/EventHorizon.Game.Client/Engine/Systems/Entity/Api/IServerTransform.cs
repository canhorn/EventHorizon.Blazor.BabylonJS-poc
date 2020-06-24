namespace EventHorizon.Game.Client.Engine.Systems.Entity.Api
{
    public interface IServerTransform
    {
        IServerVector3 Position { get; }
        IServerVector3 rotation { get; }
        IServerVector3 scaling { get; }
        long? scalingDeterminant { get; }
    }
}
