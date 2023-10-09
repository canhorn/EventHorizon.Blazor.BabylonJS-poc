namespace EventHorizon.Game.Editor.Zone.Systems.ArtifactManagement.Model;

using EventHorizon.Zone.Systems.ArtifactManagement.Model;

public class TriggerZoneArtifactBackupApiResult
{
    public string ReferenceId { get; set; } = string.Empty;

    internal TriggerZoneArtifactBackupResult ToResult()
    {
        return new TriggerZoneArtifactBackupResult(ReferenceId);
    }
}
