namespace EventHorizon.Zone.Systems.ArtifactManagement.Model;

public class TriggerZoneArtifactExportResult
{
    public string ReferenceId { get; }

    public TriggerZoneArtifactExportResult(
        string referenceId
    )
    {
        ReferenceId = referenceId;
    }
}
