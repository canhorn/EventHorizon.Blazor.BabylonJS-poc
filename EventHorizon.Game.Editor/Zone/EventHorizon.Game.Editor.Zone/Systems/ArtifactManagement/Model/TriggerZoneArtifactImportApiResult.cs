namespace EventHorizon.Game.Editor.Zone.Systems.ArtifactManagement.Model;

using EventHorizon.Zone.Systems.ArtifactManagement.Model;

public class TriggerZoneArtifactImportApiResult
{
    public string ReferenceId { get; set; } = string.Empty;

    internal TriggerZoneArtifactImportResult ToResult()
    {
        return new TriggerZoneArtifactImportResult(
            ReferenceId
        );
    }
}
