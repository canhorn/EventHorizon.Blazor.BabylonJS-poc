namespace EventHorizon.Game.Editor.Zone.Editor.Services.Connect;

using System.Threading;
using System.Threading.Tasks;

using EventHorizon.Game.Client.Core.Command.Model;
using EventHorizon.Game.Editor.Zone.Editor.Services.Api;

using MediatR;

public class StartConnectionToZoneEditorServerCommandHandler
    : IRequestHandler<
        StartConnectionToZoneEditorServerCommand,
        StandardCommandResult
    >
{
    private readonly ZoneEditorServices _zoneEditorServices;

    public StartConnectionToZoneEditorServerCommandHandler(
        ZoneEditorServices zoneEditorServices
    )
    {
        _zoneEditorServices = zoneEditorServices;
    }

    public Task<StandardCommandResult> Handle(
        StartConnectionToZoneEditorServerCommand request,
        CancellationToken cancellationToken
    )
    {
        return _zoneEditorServices.Connect(
            request.AccessToken,
            request.ZoneDetails,
            cancellationToken
        );
    }
}
