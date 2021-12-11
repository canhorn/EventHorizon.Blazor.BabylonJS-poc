namespace EventHorizon.Zone.Systems.ArtifactManagement.Model;

public class TriggerZoneArtifactImportResult
{
    public string ReferenceId { get; }

    public TriggerZoneArtifactImportResult(
        string referenceId
    )
    {
        ReferenceId = referenceId;
    }
}
