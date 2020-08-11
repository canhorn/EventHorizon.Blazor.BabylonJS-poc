namespace EventHorizon.Game.Client.Systems.ClientAssets.Model.Mesh
{
    using EventHorizon.Game.Client.Engine.Entity.Api;
    using EventHorizon.Game.Client.Systems.ClientAssets.Api;

    public class ClientAssetMeshDetails
    {
        public string AssetInstanceId { get; }
        public IClientAsset ClientAsset { get; }
        public IVector3 Position { get; }
        public bool SkipCaching { get; }

        public ClientAssetMeshDetails(
            string assetInstanceId,
            IClientAsset clientAsset,
            IVector3 position
        )
        {
            AssetInstanceId = assetInstanceId;
            ClientAsset = clientAsset;
            Position = position;
            SkipCaching = false;
        }

        public ClientAssetMeshDetails(
            string assetInstanceId,
            IClientAsset clientAsset,
            IVector3 position,
            bool skipCaching
        )
        {
            AssetInstanceId = assetInstanceId;
            ClientAsset = clientAsset;
            Position = position;
            SkipCaching = skipCaching;
        }
    }
}
