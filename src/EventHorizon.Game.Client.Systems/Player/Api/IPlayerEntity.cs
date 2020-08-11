namespace EventHorizon.Game.Client.Systems.Player.Api
{
    using EventHorizon.Game.Client.Engine.Lifecycle.Api;
    using EventHorizon.Game.Client.Engine.Systems.Entity.Api;

    public interface IPlayerEntity
        : ILifecycleEntity,
        IObjectEntity
    {
    }
}