namespace EventHorizon.Game.Editor.Client.AssetManagement.Model;

public static class AssetServerConstants
{
    // TODO: Move this into a Tier Variable.
    // A Tier variable is part of an Owners subscription,
    // when a Platform Owner is part of higher tiers they will get more benefits.
    public const long MAX_FILE_SIZE_IN_MEGABYTES = 150;
    public const long MAX_FILE_SIZE_IN_BYTES = 1024 * 1024 * MAX_FILE_SIZE_IN_MEGABYTES;
}
