namespace EventHorizon.Game.Editor.Zone.Systems.ArtifactManagement.Trigger;

using System.Threading;
using System.Threading.Tasks;

using EventHorizon.Game.Client.Core.Command.Model;
using EventHorizon.Game.Editor.Zone.Services.Api;
using EventHorizon.Zone.Systems.ArtifactManagement.Model;
using EventHorizon.Zone.Systems.ArtifactManagement.Trigger;

using MediatR;

public class TriggerZoneArtifactBackupCommandHandler
    : IRequestHandler<
        TriggerZoneArtifactBackupCommand,
        CommandResult<TriggerZoneArtifactBackupResult>
    >
{
    private readonly ZoneAdminServices _zoneAdminServices;

    public TriggerZoneArtifactBackupCommandHandler(
        ZoneAdminServices zoneAdminServices
    )
    {
        _zoneAdminServices = zoneAdminServices;
    }

    public async Task<CommandResult<TriggerZoneArtifactBackupResult>> Handle(
        TriggerZoneArtifactBackupCommand request,
        CancellationToken cancellationToken
    )
    {
        var result =
            await _zoneAdminServices.Api.ArtifactManagement.TriggerBackup(
                cancellationToken
            );
        if (result.Success.IsNotTrue() || result.Result.IsNull())
        {
            return result.ErrorCode ?? ZoneAdminErrorCodes.BAD_API_REQUEST;
        }

        return result.Result.ToResult();
    }
}
