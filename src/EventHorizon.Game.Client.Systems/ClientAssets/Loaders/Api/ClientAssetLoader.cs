namespace EventHorizon.Game.Client.Systems.ClientAssets.Loaders.Api
{
    using System.Threading.Tasks;
    using EventHorizon.Game.Client.Engine.Entity.Api;
    using EventHorizon.Game.Client.Systems.ClientAssets.Api;

    public interface ClientAssetLoader
    {
        Task Load(
            ClientAssetDetails details,
            ClientAsset clientAsset
        );
        ClientAssetDetails CreateDetails(
            string assetInstanceId, 
            ClientAsset clientAsset, 
            IVector3 position
        );
    }
}
