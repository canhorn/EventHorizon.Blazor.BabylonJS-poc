namespace EventHorizon.Game.Editor.Zone.Systems.DataStorage.Update;

using System.Threading;
using System.Threading.Tasks;
using EventHorizon.Game.Client.Core.Command.Model;
using EventHorizon.Game.Editor.Zone.Services.Api;
using EventHorizon.Zone.Systems.DataStorage.Update;
using MediatR;

public class UpdateDataStoreValueCommandHandler
    : IRequestHandler<UpdateDataStoreValueCommand, StandardCommandResult>
{
    private readonly ZoneAdminServices _zoneAdminServices;

    public UpdateDataStoreValueCommandHandler(ZoneAdminServices zoneAdminServices)
    {
        _zoneAdminServices = zoneAdminServices;
    }

    public async Task<StandardCommandResult> Handle(
        UpdateDataStoreValueCommand request,
        CancellationToken cancellationToken
    )
    {
        var result = await _zoneAdminServices.Api.DataStorage.Update(
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
