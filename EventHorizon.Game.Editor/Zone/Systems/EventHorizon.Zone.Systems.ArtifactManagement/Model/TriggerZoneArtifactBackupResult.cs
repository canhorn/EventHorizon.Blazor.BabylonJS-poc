namespace EventHorizon.Zone.Systems.ArtifactManagement.Model;

public class TriggerZoneArtifactBackupResult
{
    public string ReferenceId { get; }

    public TriggerZoneArtifactBackupResult(
        string referenceId
    )
    {
        ReferenceId = referenceId;
    }
}
