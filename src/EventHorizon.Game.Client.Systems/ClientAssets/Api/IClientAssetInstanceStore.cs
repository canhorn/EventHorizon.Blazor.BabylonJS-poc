namespace EventHorizon.Game.Client.Systems.ClientAssets.Api
{
    public interface IClientAssetInstanceStore
    {
        IClientAssetInstance Get(
            string id
        );
        void Set(
            IClientAssetInstance clientAsset
        );
        void Remove(
            string id
        );
    }
}
