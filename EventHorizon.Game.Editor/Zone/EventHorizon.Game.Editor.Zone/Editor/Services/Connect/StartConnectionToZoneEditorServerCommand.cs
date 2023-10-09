namespace EventHorizon.Game.Editor.Zone.Editor.Services.Connect;

using EventHorizon.Game.Client.Core.Command.Model;
using EventHorizon.Game.Editor.Core.Services.Model;

using MediatR;

public class StartConnectionToZoneEditorServerCommand
    : IRequest<StandardCommandResult>
{
    public string AccessToken { get; }
    public CoreZoneDetails ZoneDetails { get; }

    public StartConnectionToZoneEditorServerCommand(
        string accessToken,
        CoreZoneDetails zoneDetails
    )
    {
        AccessToken = accessToken;
        ZoneDetails = zoneDetails;
    }
}
