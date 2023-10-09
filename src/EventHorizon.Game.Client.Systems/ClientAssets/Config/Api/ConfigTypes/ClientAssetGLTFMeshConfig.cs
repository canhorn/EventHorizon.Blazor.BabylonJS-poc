namespace EventHorizon.Game.Client.Systems.ClientAssets.Config.Api.ConfigTypes;

using EventHorizon.Game.Client.Systems.ClientAssets.Api;

public interface ClientAssetGLTFMeshConfig : ClientAssetConfig
{
    string Path { get; }
    string File { get; }
    decimal HeightOffset { get; }
}
