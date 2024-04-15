namespace EventHorizon.Game.Editor.Zone.AdminClientAction.State;

using System;
using System.Collections.Generic;
using System.Linq;
using EventHorizon.Game.Editor.Zone.AdminClientAction.Api;
using EventHorizon.Game.Editor.Zone.AdminClientAction.Attributes;
using EventHorizon.Game.Editor.Zone.AdminClientAction.Model;
using Microsoft.Extensions.Logging;

public class StandardAdminClientActionState : AdminClientActionState
{
    private readonly ILogger _logger;
    private readonly IDictionary<string, Type> _actionTypes;

    public StandardAdminClientActionState(ILogger<StandardAdminClientActionState> logger)
    {
        _logger = logger;
        _actionTypes = new Dictionary<string, Type>();

        var clientActionTypeInfoList = AppDomain
            .CurrentDomain.GetAssemblies()
            .SelectMany(x => x.DefinedTypes)
            .Where(type => typeof(IAdminClientAction).IsAssignableFrom(type));

        foreach (var typeInfo in clientActionTypeInfoList)
        {
            var attributes = Attribute
                .GetCustomAttributes(typeInfo, typeof(AdminClientActionAttribute))
                .Cast<AdminClientActionAttribute>();

            foreach (var clientActionAttribute in attributes)
            {
                _actionTypes.Add(clientActionAttribute.Name, typeInfo);
            }
        }

        var clientActionNameList = string.Join("\n\r\t", _actionTypes.Keys);

        _logger.LogDebug(
            "AdminClientAction Registered: \n\r\t {AdminClientActionNameList}",
            clientActionNameList
        );
    }

    public Option<IAdminClientAction> Get(string actionName, IDictionary<string, object> data)
    {
        try
        {
            if (_actionTypes.TryGetValue(actionName, out var actionType))
            {
                return Activator
                    .CreateInstance(actionType, new AdminClientActionDataResolver(data))!
                    .To<IAdminClientAction>()!
                    .ToOption();
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "error");
        }
        return new Option<IAdminClientAction>(null);
    }
}
