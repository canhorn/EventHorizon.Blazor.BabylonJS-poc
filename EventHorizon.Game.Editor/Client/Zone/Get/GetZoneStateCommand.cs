namespace EventHorizon.Game.Editor.Client.Zone.Get;

using EventHorizon.Game.Client.Core.Command.Model;
using EventHorizon.Game.Editor.Client.Zone.Api;
using EventHorizon.Game.Editor.Core.Services.Model;
using MediatR;

public struct GetZoneStateCommand : IRequest<CommandResult<ZoneState>>
{
    public CoreZoneDetails ZoneDetails { get; }

    public GetZoneStateCommand(CoreZoneDetails zoneDetails)
    {
        ZoneDetails = zoneDetails;
    }
}
