using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using EventHorizon.Game.Client.Engine.Systems.ServerModule.Actions;
using MediatR;

namespace EventHorizon.Game.Client.Engine.Systems.ServerModule.Reload
{
    public class ServerModuleSystemReloadedClientActionHandler
        : INotificationHandler<ServerModuleSystemReloadedClientAction>
    {
        public Task Handle(
            ServerModuleSystemReloadedClientAction notification, 
            CancellationToken cancellationToken
        )
        {
            // TODO: [ClientAction] : Finish Implementation
            throw new NotImplementedException();
        }
    }
}
