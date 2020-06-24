using EventHorizon.Game.Client.Engine.Lifecycle.Api;

namespace EventHorizon.Game.Client.Engine.Systems.Scripting.Api
{
    public interface IServerModule 
        : IInitializableEntity,
        IDisposableEntity,
        IUpdatableEntity
    {
        string Name { get; }
    }
}
