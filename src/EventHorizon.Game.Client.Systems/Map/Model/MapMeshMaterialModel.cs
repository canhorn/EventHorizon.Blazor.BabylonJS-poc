namespace EventHorizon.Game.Client.Systems.Map.Model;

using EventHorizon.Game.Client.Systems.Map.Api;

public class MapMeshMaterialModel : IMapMeshMaterialDetails
{
    public string AssetPath { get; set; } = string.Empty;
    public string ShaderId { get; set; } = string.Empty;
    public string Shader { get; set; } = string.Empty;

    public double SandLimit { get; set; }
    public double RockLimit { get; set; }
    public double SnowLimit { get; set; }

    public AssetTextureModel GroundTexture { get; set; } =
        new AssetTextureModel();
    IAssetTextureDetails IMapMeshMaterialDetails.GroundTexture => GroundTexture;
    public AssetTextureModel GrassTexture { get; set; } =
        new AssetTextureModel();
    IAssetTextureDetails IMapMeshMaterialDetails.GrassTexture => GrassTexture;
    public AssetTextureModel SnowTexture { get; set; } =
        new AssetTextureModel();
    IAssetTextureDetails IMapMeshMaterialDetails.SnowTexture => SnowTexture;
    public AssetTextureModel SandTexture { get; set; } =
        new AssetTextureModel();
    IAssetTextureDetails IMapMeshMaterialDetails.SandTexture => SandTexture;
    public AssetTextureModel RockTexture { get; set; } =
        new AssetTextureModel();
    IAssetTextureDetails IMapMeshMaterialDetails.RockTexture => RockTexture;
    public AssetTextureModel BlendTexture { get; set; } =
        new AssetTextureModel();
    IAssetTextureDetails IMapMeshMaterialDetails.BlendTexture => BlendTexture;
}
