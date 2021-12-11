namespace EventHorizon.Game.Editor.Client.ArtifactManagement.Zone.Model;

public static class ZoneServerUploadConstants
{
    // TODO: Move this into a Tier Variable.
    // A Tier variable is part of an Owners subscription,
    // when a Platform Owner is part of higher tiers they will get more benefits.
    public const long ZONE_SERVER_MAX_FILE_SIZE_IN_MEGABYTES = 150;
    public const long ZONE_SERVER_MAX_FILE_SIZE_IN_BYTES = 1024 * 1024 * ZONE_SERVER_MAX_FILE_SIZE_IN_MEGABYTES;
}
