using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using EventHorizon.Game.Client.Engine.Systems.ServerModule.Add;
using MediatR;

namespace EventHorizon.Game.Client.Engine.Systems.ServerModule.Reload
{
    public class ClientScriptsSystemFinishedReloadingRebuildServerModuleSystemHandler
        : INotificationHandler<ClientScriptsSystemFinishedReloading>
    {
        private readonly IServerModuleState _state;

        public ClientScriptsSystemFinishedReloadingRebuildServerModuleSystemHandler(
            IServerModuleState state
        )
        {
            _state = state;
        }

        public Task Handle(
            ClientScriptsSystemFinishedReloading notification, 
            CancellationToken cancellationToken
        )
        {
            var allServerModules = _state.All();
            foreach (var serverModule in allServerModules)
            {
                serverModule.Dispose();
                serverModule.Initialize();
                // TODO: Add a validation check
                // If invalid, remove from _state;
            }

            return Task.CompletedTask;
        }
    }
}
