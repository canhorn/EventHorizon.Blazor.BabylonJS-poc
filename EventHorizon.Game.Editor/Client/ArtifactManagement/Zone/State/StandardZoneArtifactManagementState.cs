namespace EventHorizon.Game.Editor.Client.ArtifactManagement.Zone.State;

using EventHorizon.Game.Editor.Client.ArtifactManagement.Zone.Api;

public class StandardZoneArtifactManagementState : ZoneArtifactManagementState
{
    public string BackupReferenceId { get; private set; } = string.Empty;
    public string ExportReferenceId { get; private set; } = string.Empty;
    public string ImportReferenceId { get; private set; } = string.Empty;

    public void SetBackupReferenceId(string referenceId)
    {
        if (referenceId.IsNull())
        {
            return;
        }

        BackupReferenceId = referenceId;
    }

    public void SetExportReferenceId(string referenceId)
    {
        if (referenceId.IsNull())
        {
            return;
        }

        ExportReferenceId = referenceId;
    }

    public void SetImportReferenceId(string referenceId)
    {
        if (referenceId.IsNull())
        {
            return;
        }

        ImportReferenceId = referenceId;
    }
}
