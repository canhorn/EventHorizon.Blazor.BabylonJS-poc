namespace EventHorizon.Game.Editor.Zone.Systems.DataStorage.Delete;

using System;
using System.Threading;
using System.Threading.Tasks;

using EventHorizon.Game.Client.Core.Command.Model;
using EventHorizon.Game.Editor.Zone.Services.Api;
using EventHorizon.Zone.Systems.DataStorage.Delete;

using MediatR;

public class DeleteDataStoreValueCommandHandler
    : IRequestHandler<DeleteDataStoreValueCommand, StandardCommandResult>
{
    private readonly ZoneAdminServices _zoneAdminServices;

    public DeleteDataStoreValueCommandHandler(
        ZoneAdminServices zoneAdminServices
    )
    {
        _zoneAdminServices = zoneAdminServices;
    }

    public async Task<StandardCommandResult> Handle(
        DeleteDataStoreValueCommand request,
        CancellationToken cancellationToken
    )
    {
        var result = await _zoneAdminServices.Api.DataStorage.Delete(
            request.Key,
            cancellationToken
        );
        if (result.Success.IsNotTrue())
        {
            return result.ErrorCode ?? ZoneAdminErrorCodes.BAD_API_REQUEST;
        }

        return new();
    }
}
