namespace EventHorizon.Game.Client.Engine.Systems.ClientAction.Api;

using System.Collections.Generic;
using System.Reflection;

using static EventHorizon.Game.Client.Engine.Systems.ClientAction.State.StandardClientActionState;

public interface ClientActionState
{
    Option<IClientAction> Get(
        string actionName,
        IDictionary<string, object> data
    );
    Option<ExternalClientAction> GetExternal(
        string actionName,
        IDictionary<string, object> data
    );
    void LoadExternalClientActions(Assembly assembly);
}
