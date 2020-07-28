namespace EventHorizon.Game.Client.Engine.Lifecycle.Model
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Numerics;
    using System.Threading.Tasks;
    using BabylonJS;
    using EventHorizon.Game.Client.Core.Builder.Api;
    using EventHorizon.Game.Client.Engine.Core.Api;
    using EventHorizon.Game.Client.Engine.Entity.Api;
    using EventHorizon.Game.Client.Engine.Entity.Model;
    using EventHorizon.Game.Client.Engine.Lifecycle.Api;
    using EventHorizon.Game.Client.Engine.Systems.Entity.Api;

    public abstract class LifecycleEntityBase
        : ClientEntityBase, ILifecycleEntity
    {
        private readonly IObjectEntityDetails _details;

        public long EntityId => _details.Id;
        public string Name => _details.Name;
        public string GlobalId => _details.GlobalId;
        public string Type => _details.Type;
        public ITransform Transform { get; }
        public IList<string> Tags { get; } = new List<string>().AsReadOnly();
        public IDictionary<string, object> Data { get; } = new ReadOnlyDictionary<string, object>(new Dictionary<string, object>());

        public LifecycleEntityBase(
            long clientId,
            IObjectEntityDetails details,
            IBuilder<ITransform, IServerTransform> transformBuilder
        ) : base(clientId)
        {
            _details = details;
            Transform = transformBuilder.Build(
                details.Transform
            );
            Tags = new List<string>(
                details.TagList
            ).AsReadOnly();
            Data = new ReadOnlyDictionary<string, object>(
                details.Data
            );
        }

        public LifecycleEntityBase(
            IObjectEntityDetails details
        ) : this(
            GameServiceProvider.GetService<IIndexPool>().NextIndex(),
            details,
            GameServiceProvider.GetService<IBuilder<ITransform, IServerTransform>>()
        )
        { }

        public LifecycleEntityBase(
            long clientId,
            IObjectEntityDetails details
        ) : this(
            clientId,
            details,
            GameServiceProvider.GetService<IBuilder<ITransform, IServerTransform>>()
        )
        { }

        public abstract Task Initialize();
        public abstract Task PostInitialize();
        public abstract Task Dispose();
        public abstract Task Draw();
        public abstract Task Update();
    }
}
