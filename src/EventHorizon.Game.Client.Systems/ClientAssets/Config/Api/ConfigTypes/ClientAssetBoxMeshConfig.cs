namespace EventHorizon.Game.Client.Systems.ClientAssets.Config.Api.ConfigTypes
{
    using EventHorizon.Game.Client.Systems.ClientAssets.Api;

    public interface ClientAssetBoxMeshConfig
        : ClientAssetConfig
    {
        int Size { get; }
    }
}
