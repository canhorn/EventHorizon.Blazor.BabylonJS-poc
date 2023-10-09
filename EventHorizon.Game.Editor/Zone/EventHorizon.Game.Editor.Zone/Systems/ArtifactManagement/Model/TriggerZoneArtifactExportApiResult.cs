namespace EventHorizon.Game.Editor.Zone.Systems.ArtifactManagement.Model;

using EventHorizon.Zone.Systems.ArtifactManagement.Model;

public class TriggerZoneArtifactExportApiResult
{
    public string ReferenceId { get; set; } = string.Empty;

    internal TriggerZoneArtifactExportResult ToResult()
    {
        return new TriggerZoneArtifactExportResult(ReferenceId);
    }
}
