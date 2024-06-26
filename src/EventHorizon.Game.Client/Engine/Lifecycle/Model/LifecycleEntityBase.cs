﻿namespace EventHorizon.Game.Client.Engine.Lifecycle.Model;

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

public abstract class LifecycleEntityBase : ClientEntityBase, ILifecycleEntity
{
    protected IObjectEntityDetails _details;

    public long EntityId => _details.Id;
    public string Name => _details.Name;
    public string GlobalId => _details.GlobalId;
    public string Type => _details.Type;
    public ITransform Transform { get; }
    public IList<string> Tags { get; private set; } = new List<string>().AsReadOnly();
    public IObjectEntityDetails Details => _details;
    public IDictionary<string, object> Data { get; private set; } =
        new ReadOnlyDictionary<string, object>(new Dictionary<string, object>());

    public LifecycleEntityBase(
        long clientId,
        IObjectEntityDetails details,
        IBuilder<ITransform, IServerTransform> transformBuilder
    )
        : base(clientId)
    {
        Transform = transformBuilder.Build(details.Transform);
        _details = UpdateDetails(details);
    }

    public LifecycleEntityBase(IObjectEntityDetails details)
        : this(
            GameServiceProvider.GetService<IIndexPool>().NextIndex(),
            details,
            GameServiceProvider.GetService<IBuilder<ITransform, IServerTransform>>()
        ) { }

    public LifecycleEntityBase(long clientId, IObjectEntityDetails details)
        : this(
            clientId,
            details,
            GameServiceProvider.GetService<IBuilder<ITransform, IServerTransform>>()
        ) { }

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

    #region Module
    protected IDictionary<string, IModule> _moduleMap = new Dictionary<string, IModule>();

    public void RegisterModule(string name, IModule module)
    {
        _moduleMap[name] = module;
    }

    [return: MaybeNull]
    public T GetModule<T>(string name)
        where T : IModule
    {
        if (_moduleMap.TryGetValue(name, out var module))
        {
            if (module is T typedModule)
            {
                return typedModule;
            }
#if DEBUG
            GamePlatform
                .Logger<LifecycleEntityBase>()
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
            GamePlatform
                .Logger<LifecycleEntityBase>()
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

    public bool RemoveModule<T>(string name, [NotNullWhen(true)] out T? module)
        where T : IModule
    {
        module = GetModule<T>(name);
        if (module is not null)
        {
            _moduleMap.Remove(name);
            return true;
        }

        return false;
    }
    #endregion

    #region Property
    protected IDictionary<string, object> _propertyMap = new Dictionary<string, object>();

    public void SetProperty(string name, object property)
    {
        _propertyMap[name] = property;
    }

    public Option<T> GetPropertyAsOption<T>(string name) => new(GetProperty<T>(name));

    // TODO: Use Option<T> to get rid of MaybeNull
    public T? GetProperty<T>(string name)
    {
        if (_propertyMap.TryGetValue(name, out var property))
        {
            if (property is T typedProperty)
            {
                return typedProperty;
            }
        }

        if (Data.TryGetValue(name, out var dataProperty))
        {
            // This is a ObjectProperty specific rule
            // An ObjectProperty is for Dictionary based state,
            // making it easier to create dynamic scripts.
            if (typeof(ObjectProperty).IsAssignableFrom(typeof(T)))
            {
                var value = Activator.CreateInstance(
                    typeof(T),
                    dataProperty.To<Dictionary<string, object>>()
                );
                if (value.IsNotNull())
                {
                    _propertyMap.Add(name, value);
                    if (value is T typedValue)
                    {
                        return typedValue;
                    }
                }
            }
            var mapper = GameServiceProvider.GetService__UNSAFE<IMapper<T>>();
            if (mapper.IsNotNull())
            {
                var value = mapper.Map(dataProperty);
                if (value != null)
                {
                    _propertyMap.Add(name, value);
                    return value;
                }
            }
            else if (dataProperty.To<T>() is T typedDataProperty)
            {
                _propertyMap.Add(name, typedDataProperty);
                return typedDataProperty;
            }
        }
#if DEBUG
        GamePlatform
            .Logger<LifecycleEntityBase>()
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
    #endregion

    public IObjectEntityDetails UpdateDetails(IObjectEntityDetails details)
    {
        _details = details;
        var baseTags = new List<string>
        {
            TagBuilder.CreateTypeTag(_details.Type ?? TagBuilder.UNDEFINED),
            TagBuilder.CreateGlobalIdTag(_details.GlobalId ?? TagBuilder.UNDEFINED),
        };
        if (_details.Id != IObjectEntityDetails.DEFAULT_ID)
        {
            baseTags.Add(TagBuilder.CreateIdTag(_details.Id.ToString()));
            baseTags.Add(TagBuilder.CreateEntityIdTag(_details.Id.ToString()));
        }
        Tags = baseTags.Concat(details.TagList).ToList().AsReadOnly();
        Data = new ReadOnlyDictionary<string, object>(details.Data);
        // Remove all Data keys from _propertyList
        foreach (var dataKey in Data.Keys)
        {
            _propertyMap.Remove(dataKey);
        }
        return details;
    }
}
