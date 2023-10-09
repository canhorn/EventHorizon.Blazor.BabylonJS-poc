namespace EventHorizon.Game.Editor.Zone.Editor.Services.Api;

using System.Threading;
using System.Threading.Tasks;

using EventHorizon.Game.Client.Core.Command.Model;
using EventHorizon.Game.Editor.Core.Services.Model;

public interface ZoneEditorServices
{
    ZoneEditorApi Api { get; }

    Task<StandardCommandResult> Connect(
        string accessToken,
        CoreZoneDetails zoneDetails,
        CancellationToken cancellationToken
    );
    Task<StandardCommandResult> Disconnect();
}
