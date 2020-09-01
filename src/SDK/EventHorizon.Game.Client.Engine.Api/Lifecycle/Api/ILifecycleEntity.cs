namespace EventHorizon.Game.Client.Engine.Lifecycle.Api
{
    using System.Collections.Generic;

    public interface ILifecycleEntity
        : IInitializableEntity,
        IDisposableEntity,
        IDrawableEntity,
        IUpdatableEntity
    {
        IList<string> Tags { get; }
    }
}
