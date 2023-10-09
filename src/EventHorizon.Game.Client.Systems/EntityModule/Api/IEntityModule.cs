namespace EventHorizon.Game.Client.Systems.EntityModule.Api;

using EventHorizon.Game.Client.Engine.Systems.Module.Api;

public interface IEntityModule : IModule
{
    string Name { get; }

    bool IsInitializable { get; }
    bool IsDisposable { get; }
    bool IsUpdatable { get; }
}
