namespace EventHorizon.Game.Editor.Client.ArtifactManagement.Zone.Api;

public interface ZoneArtifactManagementState
{
    string BackupReferenceId { get; }
    string ExportReferenceId { get; }
    string ImportReferenceId { get; }
    void SetBackupReferenceId(string referenceId);
    void SetExportReferenceId(string referenceId);
    void SetImportReferenceId(string referenceId);
}
