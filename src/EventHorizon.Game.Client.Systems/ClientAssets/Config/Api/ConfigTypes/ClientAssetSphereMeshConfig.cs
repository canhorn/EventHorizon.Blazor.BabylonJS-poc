namespace EventHorizon.Game.Client.Systems.ClientAssets.Config.Api.ConfigTypes;

using EventHorizon.Game.Client.Systems.ClientAssets.Api;

public interface ClientAssetSphereMeshConfig : ClientAssetConfig
{
    float Segments { get; }
    float Diameter { get; }
}
