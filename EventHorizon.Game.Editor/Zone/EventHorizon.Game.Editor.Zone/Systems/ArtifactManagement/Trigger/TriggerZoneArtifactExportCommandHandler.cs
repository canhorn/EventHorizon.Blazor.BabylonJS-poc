namespace EventHorizon.Game.Editor.Zone.Systems.ArtifactManagement.Trigger;

using System.Threading;
using System.Threading.Tasks;

using EventHorizon.Game.Client.Core.Command.Model;
using EventHorizon.Game.Editor.Zone.Services.Api;
using EventHorizon.Zone.Systems.ArtifactManagement.Model;
using EventHorizon.Zone.Systems.ArtifactManagement.Trigger;

using MediatR;

public class TriggerZoneArtifactExportCommandHandler
    : IRequestHandler<TriggerZoneArtifactExportCommand, CommandResult<TriggerZoneArtifactExportResult>>
{
    private readonly ZoneAdminServices _zoneAdminServices;

    public TriggerZoneArtifactExportCommandHandler(
        ZoneAdminServices zoneAdminServices
    )
    {
        _zoneAdminServices = zoneAdminServices;
    }

    public async Task<CommandResult<TriggerZoneArtifactExportResult>> Handle(
        TriggerZoneArtifactExportCommand request,
        CancellationToken cancellationToken
    )
    {
        var result = await _zoneAdminServices.Api.ArtifactManagement.TriggerExport(
            cancellationToken
        );
        if (result.Success.IsNotTrue()
            || result.Result.IsNull()
        )
        {
            return result.ErrorCode
                ?? ZoneAdminErrorCodes.BAD_API_REQUEST;
        }

        return result.Result.ToResult();
    }
}
