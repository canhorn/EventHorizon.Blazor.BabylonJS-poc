namespace EventHorizon.Game.Editor.Zone.Services.Connect;

using System.Threading;
using System.Threading.Tasks;

using EventHorizon.Game.Client.Core.Command.Model;
using EventHorizon.Game.Editor.Zone.Services.Api;

using MediatR;

public class StopZoneServerConnectionCommandHandler
    : IRequestHandler<StopZoneServerConnectionCommand, StandardCommandResult>
{
    private readonly ZoneAdminServices _zoneAdminServices;

    public StopZoneServerConnectionCommandHandler(
        ZoneAdminServices zoneAdminServices
    )
    {
        _zoneAdminServices = zoneAdminServices;
    }

    public Task<StandardCommandResult> Handle(
        StopZoneServerConnectionCommand request,
        CancellationToken cancellationToken
    )
    {
        return _zoneAdminServices.Disconnect();
    }
}
