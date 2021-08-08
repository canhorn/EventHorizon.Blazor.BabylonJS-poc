namespace EventHorizon.Game.Server.Asset.Connect
{
    using System.Threading;
    using System.Threading.Tasks;
    using EventHorizon.Game.Client.Core.Command.Model;
    using EventHorizon.Game.Server.Asset.Api;
    using MediatR;

    public class StartConnectionToAssetServerAdminCommandHandler
        : IRequestHandler<StartConnectionToAssetServerAdminCommand, StandardCommandResult>
    {
        private readonly AssetServerAdminService _services;

        public StartConnectionToAssetServerAdminCommandHandler(
            AssetServerAdminService services
        )
        {
            _services = services;
        }

        public Task<StandardCommandResult> Handle(
            StartConnectionToAssetServerAdminCommand request,
            CancellationToken cancellationToken
        )
        {
            return _services.Connect(
                request.AccessToken,
                cancellationToken
            );
        }
    }
}
