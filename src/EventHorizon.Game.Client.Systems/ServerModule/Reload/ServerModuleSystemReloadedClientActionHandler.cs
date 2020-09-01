namespace EventHorizon.Game.Client.Systems.ServerModule.Reload
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using EventHorizon.Game.Client.Systems.ServerModule.Actions;
    using MediatR;

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
