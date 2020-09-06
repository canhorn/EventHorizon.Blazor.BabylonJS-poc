namespace EventHorizon.Game.Client.Engine.Lifecycle.Model
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Diagnostics.CodeAnalysis;
    using System.Diagnostics.Contracts;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Numerics;
    using System.Threading.Tasks;
    using BabylonJS;
    using EventHorizon.Game.Client.Core.Builder.Api;
    using EventHorizon.Game.Client.Core.Exceptions;
    using EventHorizon.Game.Client.Core.Mapper.Api;
    using EventHorizon.Game.Client.Engine.Core.Api;
    using EventHorizon.Game.Client.Engine.Entity.Api;
    using EventHorizon.Game.Client.Engine.Entity.Model;
    using EventHorizon.Game.Client.Engine.Entity.Tag;
    using EventHorizon.Game.Client.Engine.Lifecycle.Api;
    using EventHorizon.Game.Client.Engine.Systems.Entity.Api;
    using EventHorizon.Game.Client.Engine.Systems.Module.Api;

    public abstract class LifecycleEntityBase
        : ClientEntityBase,
        ILifecycleEntity
    {
        protected IObjectEntityDetails _details;

        public long EntityId => _details.Id;
        public string Name => _details.Name;
        public string GlobalId => _details.GlobalId;
        public string Type => _details.Type;
        public ITransform Transform { get; }
        public IList<string> Tags { get; private set; } = new List<string>().AsReadOnly();
        public IObjectEntityDetails Details => _details;
        public IDictionary<string, object> Data { get; private set; } = new ReadOnlyDictionary<string, object>(new Dictionary<string, object>());

        public LifecycleEntityBase(
            long clientId,
            IObjectEntityDetails details,
            IBuilder<ITransform, IServerTransform> transformBuilder
        ) : base(clientId)
        {
            Transform = transformBuilder.Build(
                details.Transform
            );
            UpdateDetails(
                details
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
        public virtual async Task PostInitialize()
        {
            foreach (var module in _moduleMap.Values.OrderBy(a => a.Priority))
            {
                await module.Initialize();
            }
        }
        public virtual async Task Dispose()
        {
            foreach (var module in _moduleMap.Values)
            {
                await module.Dispose();
            }
        }
        public abstract Task Draw();
        public virtual async Task Update()
        {
            foreach (var module in _moduleMap.Values)
            {
                await module.Update();
            }
        }

        protected IDictionary<string, IModule> _moduleMap = new Dictionary<string, IModule>();
        public void RegisterModule(
            string name,
            IModule module
        )
        {
            _moduleMap[name] = module;
        }

        [return: MaybeNull]
        public T GetModule<T>(
            string name
        ) where T : IModule
        {
            if (_moduleMap.TryGetValue(
                name,
                out var module
            ))
            {
                if (module is T typedModule)
                {
                    return typedModule;
                }
            }

#if DEBUG
            // TODO: Log Error??
            new GameException(
                "module_not_found",
                $"Was not able to find Module on entity: EntityId = {EntityId} | GlobalId = {GlobalId} | Property: {name}"
            );
#endif
            return default;
        }

        protected IDictionary<string, object> _propertyMap = new Dictionary<string, object>();
        public void SetProperty(
            string name,
            object property
        )
        {
            _propertyMap[name] = property;
        }

        [return: MaybeNull]
        // TODO: Use Option<T> to get rid of MaybeNull
        public T GetProperty<T>(
            string name
        )
        {
            if (_propertyMap.TryGetValue(
                name,
                out var property
            ))
            {
                if (property is T typedProperty)
                {
                    return typedProperty;
                }
            }
            if (Data.TryGetValue(
                name,
                out var dataProperty
            ))
            {
                try
                {
                    var mapper = GameServiceProvider.GetService<IMapper<T>>();
                    var value = mapper.Map(
                        dataProperty
                    );
                    if (value != null)
                    {
                        _propertyMap.Add(
                            name,
                            value
                        );
                        return value;
                    }
                }
                catch (Exception ex)
                {
                    // TODO: ignore for now
                }

                try
                {
                    if (dataProperty.Cast<T>() is T typedDataProperty)
                    {
                        _propertyMap.Add(
                            name,
                            typedDataProperty
                        );
                        return typedDataProperty;
                    }
                }
                catch (Exception ex)
                {
                    // TODO: ignore for now
                }
            }
#if DEBUG
            // TODO: Log Error??
            new GameException(
                "property_not_found",
                $"Was not able to find Property on entity: EntityId = {EntityId} | GlobalId = {GlobalId} | Property: {name}"
            );
#endif
            return default;
        }

        public void UpdateDetails(
            IObjectEntityDetails details
        )
        {
            _details = details;
            Tags = new List<string>(
                new List<string>
                {
                    TagBuilder.CreateTypeTag(_details.Type ?? TagBuilder.UNDEFINED),
                    TagBuilder.CreateIdTag(_details.Id.ToString()),
                    TagBuilder.CreateEntityIdTag(_details.Id.ToString()),
                    TagBuilder.CreateGlobalIdTag(_details.GlobalId ?? TagBuilder.UNDEFINED),
                }
            ).Concat(
                details.TagList
            ).ToList().AsReadOnly();
            Data = new ReadOnlyDictionary<string, object>(
                details.Data
            );
            // Remove all Data keys from _propertyList
        }
    }
}
