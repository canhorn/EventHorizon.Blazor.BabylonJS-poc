namespace EventHorizon.Game.Client.Engine.Systems.Entity.Model
{
    using EventHorizon.Game.Client.Engine.Systems.Entity.Api;

    public class ServerTransform
        : IServerTransform
    {
        public ServerVector3 Position { get; set; } = ServerVector3.Zero();
        IServerVector3 IServerTransform.Position => Position;
        public ServerVector3 Rotation { get; set; } = ServerVector3.Zero();
        IServerVector3 IServerTransform.Rotation => Rotation;
        public ServerVector3 Scaling { get; set; } = ServerVector3.One();
        IServerVector3 IServerTransform.Scaling => Scaling;
        public long? ScalingDeterminant { get; set; }
    }
}