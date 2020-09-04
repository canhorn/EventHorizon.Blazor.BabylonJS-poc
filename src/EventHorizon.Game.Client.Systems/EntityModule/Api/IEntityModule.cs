namespace EventHorizon.Game.Client.Systems.EntityModule.Api
{
    using EventHorizon.Game.Client.Engine.Lifecycle.Api;

    public interface IEntityModule
        : IInitializableEntity,
        IDisposableEntity,
        IUpdatableEntity
    {
        string Name { get; }

        bool IsInitializable { get; }
        bool IsDisposable { get; }
        bool IsUpdatable { get; }
    }
}
