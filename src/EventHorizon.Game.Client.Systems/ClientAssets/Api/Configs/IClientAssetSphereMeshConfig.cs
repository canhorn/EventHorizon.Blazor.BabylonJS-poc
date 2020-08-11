namespace EventHorizon.Game.Client.Systems.ClientAssets.Api.Configs
{
    public interface IClientAssetSphereMeshConfig
        : IClientAssetConfig
    {
        float Segments { get; }
        float Diameter { get; }
    }
}
