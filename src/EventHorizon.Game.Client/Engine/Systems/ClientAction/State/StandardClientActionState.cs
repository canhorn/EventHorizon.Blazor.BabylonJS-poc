namespace EventHorizon.Game.Client.Engine.Systems.ClientAction.State
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using EventHorizon.Game.Client.Engine.Systems.ClientAction.Api;
    using EventHorizon.Game.Client.Engine.Systems.ClientAction.Attributes;
    using EventHorizon.Game.Client.Engine.Systems.ClientAction.Publish;
    using Microsoft.Extensions.Logging;

    public class StandardClientActionState
        : ClientActionState
    {
        private readonly ILogger _logger;
        private readonly IDictionary<string, Type> _actionTypes;

        public StandardClientActionState(
            ILogger<StandardClientActionState> logger
        )
        {
            _logger = logger;
            _actionTypes = new Dictionary<string, Type>();

            var clientActionTypeInfoList = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(x => x.DefinedTypes)
                .Where(type => typeof(IClientAction).IsAssignableFrom(type));

            foreach (var typeInfo in clientActionTypeInfoList)
            {
                var attributes = Attribute.GetCustomAttributes(
                    typeInfo,
                    typeof(ClientActionAttribute)
                ).Cast<ClientActionAttribute>();

                foreach (var clientActionAttribute in attributes)
                {
                    _actionTypes.Add(
                        clientActionAttribute.Name,
                        typeInfo
                    );
                }
            }

            var clientActionNameList = string.Join(
                "\n\r\t",
                _actionTypes.Keys
            );

            _logger.LogDebug(
                "ClientAction Registered: \n\r\t {ClientActionNameList}",
                clientActionNameList
            );
        }

        public Option<IClientAction> Get(
            string actionName,
            IDictionary<string, object> data
        )
        {
            try
            {
                if (_actionTypes.TryGetValue(
                    actionName,
                    out var actionType
                ))
                {
                    return Activator.CreateInstance(
                        actionType,
                        new ClientActionDataResolver(
                            data
                        )
                    )!.To<IClientAction>()!.ToOption();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(
                    ex,
                    "error"
                );
            }
            return new Option<IClientAction>(
                null
            );
        }
    }
}
