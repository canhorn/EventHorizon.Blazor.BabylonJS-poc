namespace EventHorizon.Game.Editor.Zone.Systems.DataStorage.Create;

using System.Threading;
using System.Threading.Tasks;

using EventHorizon.Game.Client.Core.Command.Model;
using EventHorizon.Game.Editor.Zone.Services.Api;
using EventHorizon.Zone.Systems.DataStorage.Create;

using MediatR;

public class CreateDataStoreValueCommandHandler
    : IRequestHandler<CreateDataStoreValueCommand, StandardCommandResult>
{
    private readonly ZoneAdminServices _zoneAdminServices;

    public CreateDataStoreValueCommandHandler(
        ZoneAdminServices zoneAdminServices
    )
    {
        _zoneAdminServices = zoneAdminServices;
    }

    public async Task<StandardCommandResult> Handle(
        CreateDataStoreValueCommand request,
        CancellationToken cancellationToken
    )
    {
        var result = await _zoneAdminServices.Api.DataStorage.Create(
            request.Key,
            request.Type,
            request.Value,
            cancellationToken
        );
        if (result.Success.IsNotTrue())
        {
            return result.ErrorCode ?? ZoneAdminErrorCodes.BAD_API_REQUEST;
        }

        return new();
    }
}
