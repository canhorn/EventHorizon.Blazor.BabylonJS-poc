namespace EventHorizon.Game.Client.Systems.ClientAssets.Api
{
    public interface IClientAssetStore
    {
        Option<IClientAsset> Get(
            string id
        );
        void Set(
            IClientAsset clientAsset
        );
    }
}
