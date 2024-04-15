namespace EventHorizon.Game.Editor.Zone.Services.Connect;

using System;
using System.Threading;
using System.Threading.Tasks;
using EventHorizon.Game.Client.Core.Command.Model;
using EventHorizon.Game.Editor.Zone.Services.Api;
using MediatR;

public class StartConnectionToZoneServerCommandHandler
    : IRequestHandler<StartConnectionToZoneServerCommand, StandardCommandResult>
{
    private readonly ZoneAdminServices _zoneAdminServices;

    public StartConnectionToZoneServerCommandHandler(ZoneAdminServices zoneAdminServices)
    {
        _zoneAdminServices = zoneAdminServices;
    }

    public Task<StandardCommandResult> Handle(
        StartConnectionToZoneServerCommand request,
        CancellationToken cancellationToken
    )
    {
        return _zoneAdminServices.Connect(
            request.AccessToken,
            request.ZoneDetails,
            cancellationToken
        );
    }
}
