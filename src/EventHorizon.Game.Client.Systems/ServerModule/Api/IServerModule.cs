namespace EventHorizon.Game.Client.Systems.ServerModule.Api
{
    using EventHorizon.Game.Client.Engine.Lifecycle.Api;

    public interface IServerModule
        : IInitializableEntity,
        IDisposableEntity,
        IUpdatableEntity
    {
        string Name { get; }
    }
}
