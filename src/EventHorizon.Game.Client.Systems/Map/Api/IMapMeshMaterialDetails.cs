namespace EventHorizon.Game.Client.Systems.Map.Api;

public interface IMapMeshMaterialDetails
{
    string AssetPath { get; }
    string ShaderId { get; }
    string Shader { get; }

    double SandLimit { get; }
    double RockLimit { get; }
    double SnowLimit { get; }

    IAssetTextureDetails GroundTexture { get; }
    IAssetTextureDetails GrassTexture { get; }
    IAssetTextureDetails SnowTexture { get; }
    IAssetTextureDetails SandTexture { get; }
    IAssetTextureDetails RockTexture { get; }
    IAssetTextureDetails BlendTexture { get; }
}
