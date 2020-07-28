using EventHorizon.Game.Client.Systems.Map.Api;

namespace EventHorizon.Game.Client.Systems.Map.Model
{
    public class AssetTextureModel
        : IAssetTextureDetails
    {
        public string Image { get; set; }
        public decimal UScale { get; set; }
        public decimal VScale { get; set; }
    }
}
