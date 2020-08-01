namespace EventHorizon.Game.Client.Engine.Systems.Module.Api
{
    using EventHorizon.Game.Client.Engine.Lifecycle.Api;

    public interface IModule
        : IDisposableEntity,
        IUpdatableEntity
    {
    }
}
