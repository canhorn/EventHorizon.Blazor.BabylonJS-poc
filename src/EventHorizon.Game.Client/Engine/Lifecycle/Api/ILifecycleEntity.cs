using System;
using System.Collections.Generic;
using System.Text;

namespace EventHorizon.Game.Client.Engine.Lifecycle.Api
{
    public interface ILifecycleEntity
        : IInitializableEntity,
        IDisposableEntity,
        IDrawableEntity,
        IUpdatableEntity
    {
        IList<string> Tags { get; }
    }
}
