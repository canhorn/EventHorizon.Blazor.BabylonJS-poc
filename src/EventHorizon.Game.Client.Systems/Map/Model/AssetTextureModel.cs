namespace EventHorizon.Game.Client.Systems.Map.Model
{
    using EventHorizon.Game.Client.Systems.Map.Api;

    public class AssetTextureModel
        : IAssetTextureDetails
    {
        public string Image { get; set; } = string.Empty;
        public decimal UScale { get; set; }
        public decimal VScale { get; set; }
    }
}
