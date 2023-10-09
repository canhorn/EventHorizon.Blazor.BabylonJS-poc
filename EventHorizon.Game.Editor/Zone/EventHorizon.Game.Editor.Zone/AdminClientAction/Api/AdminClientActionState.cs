namespace EventHorizon.Game.Editor.Zone.AdminClientAction.Api;

using System.Collections.Generic;

public interface AdminClientActionState
{
    Option<IAdminClientAction> Get(
        string actionName,
        IDictionary<string, object> data
    );
}
