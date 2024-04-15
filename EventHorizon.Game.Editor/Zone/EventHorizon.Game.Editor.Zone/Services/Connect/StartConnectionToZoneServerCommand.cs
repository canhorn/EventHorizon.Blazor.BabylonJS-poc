namespace EventHorizon.Game.Editor.Zone.Services.Connect;

using EventHorizon.Game.Client.Core.Command.Model;
using EventHorizon.Game.Editor.Core.Services.Model;
using MediatR;

public struct StartConnectionToZoneServerCommand : IRequest<StandardCommandResult>
{
    public string AccessToken { get; }
    public CoreZoneDetails ZoneDetails { get; }

    public StartConnectionToZoneServerCommand(string accessToken, CoreZoneDetails zoneDetails)
    {
        AccessToken = accessToken;
        ZoneDetails = zoneDetails;
    }
}
