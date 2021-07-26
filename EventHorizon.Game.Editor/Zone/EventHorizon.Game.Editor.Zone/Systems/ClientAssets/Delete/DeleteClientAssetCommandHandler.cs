namespace EventHorizon.Game.Editor.Zone.Systems.ClientAssets.Delete
{
    using System.Threading;
    using System.Threading.Tasks;
    using EventHorizon.Game.Client.Core.Command.Model;
    using EventHorizon.Game.Editor.Zone.Services.Api;
    using EventHorizon.Zone.Systems.ClientAssets.Delete;
    using MediatR;

    public class DeleteClientAssetCommandHandler
        : IRequestHandler<DeleteClientAssetCommand, StandardCommandResult>
    {
        private readonly ZoneAdminServices _zoneAdminServices;

        public DeleteClientAssetCommandHandler(
            ZoneAdminServices zoneAdminServices
        )
        {
            _zoneAdminServices = zoneAdminServices;
        }

        public async Task<StandardCommandResult> Handle(
            DeleteClientAssetCommand request,
            CancellationToken cancellationToken
        )
        {
            var result = await _zoneAdminServices.Api.ClientAssets.Delete(
                request.Id,
                cancellationToken
            );
            if (result.Success.IsNotTrue())
            {
                return result.ErrorCode
                    ?? ZoneAdminErrorCodes.BAD_API_REQUEST;
            }

            return new();
        }
    }
}
