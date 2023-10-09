namespace EventHorizon.Game.Editor.Zone.Systems.ClientAssets.Create;

using System.Threading;
using System.Threading.Tasks;

using EventHorizon.Activity;
using EventHorizon.Game.Client.Core.Command.Model;
using EventHorizon.Game.Editor.Zone.Services.Api;
using EventHorizon.Zone.Systems.ClientAssets.Create;

using MediatR;

public class CreateClientAssetCommandHandler
    : IRequestHandler<CreateClientAssetCommand, StandardCommandResult>
{
    private readonly IMediator _mediator;
    private readonly ZoneAdminServices _zoneAdminServices;

    public CreateClientAssetCommandHandler(
        IMediator mediator,
        ZoneAdminServices zoneAdminServices
    )
    {
        _mediator = mediator;
        _zoneAdminServices = zoneAdminServices;
    }

    public async Task<StandardCommandResult> Handle(
        CreateClientAssetCommand request,
        CancellationToken cancellationToken
    )
    {
        var result = await _zoneAdminServices.Api.ClientAssets.Create(
            request.ClientAsset,
            cancellationToken
        );
        if (result.Success.IsNotTrue())
        {
            return result.ErrorCode ?? ZoneAdminErrorCodes.BAD_API_REQUEST;
        }

        await _mediator.Publish(
            new ActivityEvent("AssetManagement", "Created", "ClientAsset"),
            cancellationToken
        );

        return new();
    }
}
