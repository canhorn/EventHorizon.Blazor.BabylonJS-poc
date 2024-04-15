namespace EventHorizon.Game.Client.Engine.Lifecycle.Api;

using System.Collections.Generic;
using EventHorizon.Game.Client.Engine.Systems.Entity.Api;

public interface ILifecycleEntity
    : IObjectEntity,
        IInitializableEntity,
        IDisposableEntity,
        IDrawableEntity,
        IUpdatableEntity { }
