namespace EventHorizon.Game.Client.Engine.Systems.ClientAction.Api
{
    using System;
    using System.Collections.Generic;

    public interface ClientActionState
    {
        Option<IClientAction> Get(
            string actionName,
            IDictionary<string, object> data
        );
    }
}
