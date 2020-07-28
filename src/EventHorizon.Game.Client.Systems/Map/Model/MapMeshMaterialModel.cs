using EventHorizon.Game.Client.Systems.Map.Api;

namespace EventHorizon.Game.Client.Systems.Map.Model
{
    public class MapMeshMaterialModel
        : IMapMeshMaterialDetails
    {
        public string AssetPath { get; set; }
        public string ShaderId { get; set; }
        public string Shader { get; set; }

        public double SandLimit { get; set; }
        public double RockLimit { get; set; }
        public double SnowLimit { get; set; }

        public AssetTextureModel GroundTexture { get; set; }
        IAssetTextureDetails IMapMeshMaterialDetails.GroundTexture => GroundTexture;
        public AssetTextureModel GrassTexture { get; set; }
        IAssetTextureDetails IMapMeshMaterialDetails.GrassTexture => GrassTexture;
        public AssetTextureModel SnowTexture { get; set; }
        IAssetTextureDetails IMapMeshMaterialDetails.SnowTexture => SnowTexture;
        public AssetTextureModel SandTexture { get; set; }
        IAssetTextureDetails IMapMeshMaterialDetails.SandTexture => SandTexture;
        public AssetTextureModel RockTexture { get; set; }
        IAssetTextureDetails IMapMeshMaterialDetails.RockTexture => RockTexture;
        public AssetTextureModel BlendTexture { get; set; }
        IAssetTextureDetails IMapMeshMaterialDetails.BlendTexture => BlendTexture;
    }
}
