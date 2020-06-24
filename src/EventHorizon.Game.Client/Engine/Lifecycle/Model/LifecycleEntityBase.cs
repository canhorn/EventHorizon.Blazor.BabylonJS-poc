namespace EventHorizon.Game.Client.Engine.Lifecycle.Model
{
    using System;
    using System.Threading.Tasks;
    using EventHorizon.Game.Client.Engine.Core.Api;
    using EventHorizon.Game.Client.Engine.Entity.Model;
    using EventHorizon.Game.Client.Engine.Lifecycle.Api;
    using MediatR;

    public abstract class LifecycleEntityBase
        : ClientEntityBase,
        IInitializableEntity,
        IDisposableEntity,
        IDrawableEntity,
        IUpdatableEntity
    {
        public LifecycleEntityBase(
            long clientId
        ) : base(clientId)
        {
        }

        public LifecycleEntityBase()
            : base(GameServiceProvider.GetService<IIndexPool>().NextIndex())
        { }

        public abstract Task Initialize();
        public abstract Task PostInitialize();
        public abstract Task Dispose();
        public abstract Task Draw();
        public abstract Task Update();
    }
}
