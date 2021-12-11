namespace EventHorizon.Zone.Systems.ArtifactManagement.Finished;

using EventHorizon.Game.Editor.Zone.AdminClientAction.Api;
using EventHorizon.Game.Editor.Zone.AdminClientAction.Attributes;
using EventHorizon.Observer.Model;

[AdminClientAction("ZONE_SERVER_BACKUP_FINISHED_ADMIN_CLIENT_ACTION")]
public struct ZoneServerBackupFinishedEvent
    : IAdminClientAction
{
    public string ReferenceId { get; }
    public string BackupPath { get; }

    public ZoneServerBackupFinishedEvent(
        IAdminClientActionDataResolver resolver
    )
    {
        ReferenceId = resolver.Resolve<string>(
            "referenceId"
        );
        BackupPath = resolver.Resolve<string>(
            "backupPath"
        );
    }
}

public interface ZoneServerBackupFinishedEventObserver
    : ArgumentObserver<ZoneServerBackupFinishedEvent>
{
}
