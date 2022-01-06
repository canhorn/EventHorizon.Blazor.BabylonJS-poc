namespace EventHorizon.Zone.Systems.ArtifactManagement.Finished;

using EventHorizon.Game.Editor.Zone.AdminClientAction.Api;
using EventHorizon.Game.Editor.Zone.AdminClientAction.Attributes;
using EventHorizon.Observer.Model;

[AdminClientAction("ZONE_SERVER_BACKUP_FINISHED_ADMIN_CLIENT_ACTION")]
public struct ZoneServerBackupFinishedEvent
    : IAdminClientAction
{
    public string ReferenceId { get; }
    public string BackupUrl { get; }

    public ZoneServerBackupFinishedEvent(
        IAdminClientActionDataResolver resolver
    )
    {
        ReferenceId = resolver.Resolve<string>(
            "referenceId"
        );
        BackupUrl = resolver.Resolve<string>(
            "backupUrl"
        );
    }
}

public interface ZoneServerBackupFinishedEventObserver
    : ArgumentObserver<ZoneServerBackupFinishedEvent>
{
}
