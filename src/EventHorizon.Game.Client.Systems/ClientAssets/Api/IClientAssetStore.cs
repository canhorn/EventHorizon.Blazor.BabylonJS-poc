namespace EventHorizon.Game.Client.Systems.ClientAssets.Api
{
    public interface IClientAssetStore
    {
        IClientAsset Get(
            string id
        );
        void Set(
            IClientAsset clientAsset
        );
    }
}
