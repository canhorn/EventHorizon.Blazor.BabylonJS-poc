namespace EventHorizon.Game.Client.Engine.Systems.ClientAction.State;

using System;
using System.Collections.Generic;
using System.Linq;
using EventHorizon.Game.Client.Engine.Systems.ClientAction.Api;
using EventHorizon.Game.Client.Engine.Systems.ClientAction.Attributes;
using EventHorizon.Game.Client.Engine.Systems.ClientAction.Model;
using EventHorizon.Game.Client.Engine.Systems.ClientAction.Publish;
using EventHorizon.Observer.Model;
using Microsoft.Extensions.Logging;

public class StandardClientActionState : ClientActionState
{
    private readonly ILogger _logger;
    private readonly IDictionary<string, Type> _actionTypes;
    private readonly IDictionary<string, ExternalAction> _externalActions;

    public StandardClientActionState(ILogger<StandardClientActionState> logger)
    {
        _logger = logger;
        _actionTypes = new Dictionary<string, Type>();

        var clientActionTypeInfoList = AppDomain
            .CurrentDomain.GetAssemblies()
            .SelectMany(x => x.DefinedTypes)
            .Where(type => typeof(IClientAction).IsAssignableFrom(type));

        foreach (var typeInfo in clientActionTypeInfoList)
        {
            var attributes = Attribute
                .GetCustomAttributes(typeInfo, typeof(ClientActionAttribute))
                .Cast<ClientActionAttribute>();

            foreach (var clientActionAttribute in attributes)
            {
                _actionTypes.Add(clientActionAttribute.Name, typeInfo);
            }
        }

        var clientActionNameList = string.Join("\n\r\t", _actionTypes.Keys);

        _logger.LogDebug(
            "ClientAction Registered: \n\r\t {ClientActionNameList}",
            clientActionNameList
        );

        _externalActions = new Dictionary<string, ExternalAction>();
    }

    public Option<IClientAction> Get(string actionName, IDictionary<string, object> data)
    {
        try
        {
            if (_actionTypes.TryGetValue(actionName, out var actionType))
            {
                return Activator
                    .CreateInstance(actionType, new ClientActionDataResolver(data))!
                    .To<IClientAction>()!
                    .ToOption();
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "error");
        }
        return new Option<IClientAction>(null);
    }

    public class ExternalClientAction
    {
        public IClientAction Action { get; }
        public Type ActionType { get; }
        public Type ObserverType { get; }

        public ExternalClientAction(IClientAction action, Type actionType, Type observerType)
        {
            Action = action;
            ActionType = actionType;
            ObserverType = observerType;
        }
    }

    public Option<ExternalClientAction> GetExternal(
        string actionName,
        IDictionary<string, object> data
    )
    {
        try
        {
            if (_externalActions.TryGetValue(actionName, out var externalAction))
            {
                var clientAction = Activator
                    .CreateInstance(externalAction.Event, new ClientActionDataResolver(data))!
                    .To<IClientAction>()!
                    .ToOption();

                return new ExternalClientAction(
                    clientAction.Value!,
                    externalAction.Event,
                    externalAction.Observer
                ).ToOption();
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to lookup external action: {ActionName}", actionName);
        }

        return new Option<ExternalClientAction>(null);
    }

    public void LoadExternalClientActions(System.Reflection.Assembly assembly)
    {
        _externalActions.Clear();

        var clientActionTypeInfoList = assembly.DefinedTypes.Where(type =>
            typeof(IClientAction).IsAssignableFrom(type)
        );

        var argumentObservers = assembly.DefinedTypes.Where(type =>
            typeof(ObserverBase).IsAssignableFrom(type)
        );
        foreach (var observer in argumentObservers)
        {
            Console.WriteLine(observer.FullName + " ||| " + observer.Name);
        }

        foreach (var typeInfoType in clientActionTypeInfoList)
        {
            var attributes = Attribute
                .GetCustomAttributes(typeInfoType, typeof(ClientActionAttribute))
                .Cast<ClientActionAttribute>();

            foreach (var clientActionAttribute in attributes)
            {
                var observerType = argumentObservers.FirstOrDefault(x =>
                    x.Name == $"{typeInfoType.Name}Observer"
                );
                if (observerType == null)
                {
                    continue;
                }

                _externalActions.Add(
                    clientActionAttribute.Name,
                    new ExternalAction(typeInfoType, observerType)
                );
            }
        }
    }
}
