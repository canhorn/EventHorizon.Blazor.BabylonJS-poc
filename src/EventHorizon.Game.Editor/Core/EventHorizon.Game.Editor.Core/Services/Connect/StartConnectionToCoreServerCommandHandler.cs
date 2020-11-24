namespace EventHorizon.Game.Editor.Core.Services.Connect
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using EventHorizon.Game.Client.Core.Command.Model;
    using EventHorizon.Game.Editor.Core.Services.Api;
    using MediatR;

    public class StartConnectionToCoreServerCommandHandler
        : IRequestHandler<StartConnectionToCoreServerCommand, StandardCommandResult>
    {
        private readonly CoreAdminServices _coreAdminServices;

        public StartConnectionToCoreServerCommandHandler(
            CoreAdminServices coreAdminServices
        )
        {
            _coreAdminServices = coreAdminServices;
        }

        public Task<StandardCommandResult> Handle(
            StartConnectionToCoreServerCommand request,
            CancellationToken cancellationToken
        )
        {
            return _coreAdminServices.Connect(
                request.AccessToken,
                cancellationToken
            );
        }
    }
}
