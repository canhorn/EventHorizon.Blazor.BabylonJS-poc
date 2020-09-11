namespace EventHorizon.Game.Client.Engine.Lifecycle.Model
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;
    using System.Threading.Tasks;
    using EventHorizon.Game.Client.Core.Builder.Api;
    using EventHorizon.Game.Client.Core.Mapper.Api;
    using EventHorizon.Game.Client.Engine.Core.Api;
    using EventHorizon.Game.Client.Engine.Entity.Api;
    using EventHorizon.Game.Client.Engine.Entity.Model;
    using EventHorizon.Game.Client.Engine.Entity.Tag;
    using EventHorizon.Game.Client.Engine.Lifecycle.Api;
    using EventHorizon.Game.Client.Engine.Systems.Entity.Api;
    using EventHorizon.Game.Client.Engine.Systems.Module.Api;
    using Microsoft.Extensions.Logging;

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
            _details = UpdateDetails(
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
#if DEBUG
                GamePlatfrom.Logger<LifecycleEntityBase>()
                    .LogDebug(
                        "Module was not the correct type on entity: EntityId = {EntityId} | GlobalId = {GlobalId} | Module: {ModuleName} | Type: {ModuleType}",
                        EntityId,
                        GlobalId,
                        name,
                        typeof(T)
                    );
#endif
            }
#if DEBUG
            else
            {
                GamePlatfrom.Logger<LifecycleEntityBase>()
                    .LogDebug(
                        "Module was not found on entity: EntityId = {EntityId} | GlobalId = {GlobalId} | Module: {ModuleName} | Type: {ModuleType}",
                        EntityId,
                        GlobalId,
                        name,
                        typeof(T)
                    );
            }
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

        public Option<T> GetPropertyAsOption<T>(
            string name
        ) => new Option<T>(
            GetProperty<T>(name)
        );

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
                var mapper = GameServiceProvider.GetService__UNSAFE<IMapper<T>>();
                if (mapper.IsNotNull())
                {
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
                else if (dataProperty.Cast<T>() is T typedDataProperty)
                {
                    _propertyMap.Add(
                        name,
                        typedDataProperty
                    );
                    return typedDataProperty;
                }
            }
#if DEBUG
            GamePlatfrom.Logger<LifecycleEntityBase>()
                .LogDebug(
                    "Property was not found on entity: EntityId = {EntityId} | GlobalId = {GlobalId} | Property: {PropertyName} | Type: {PropertyType}",
                    EntityId,
                    GlobalId,
                    name,
                    typeof(T)
                );
#endif
            return default;
        }

        public IObjectEntityDetails UpdateDetails(
            IObjectEntityDetails details
        )
        {
            _details = details;
            var baseTags = new List<string>
            {
                TagBuilder.CreateTypeTag(_details.Type ?? TagBuilder.UNDEFINED),
                TagBuilder.CreateGlobalIdTag(_details.GlobalId ?? TagBuilder.UNDEFINED),
            };
            if (_details.Id != IObjectEntityDetails.DEFAULT_ID)
            {
                baseTags.Add(
                    TagBuilder.CreateIdTag(_details.Id.ToString())
                );
                baseTags.Add(
                    TagBuilder.CreateEntityIdTag(_details.Id.ToString())
                );
            }
            Tags = baseTags.Concat(
                details.TagList
            ).ToList().AsReadOnly();
            Data = new ReadOnlyDictionary<string, object>(
                details.Data
            );
            // Remove all Data keys from _propertyList
            foreach (var dataKey in Data.Keys)
            {
                _propertyMap.Remove(dataKey);
            }
            return details;
        }
    }
}
