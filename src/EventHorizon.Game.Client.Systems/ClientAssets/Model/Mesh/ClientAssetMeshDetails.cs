namespace EventHorizon.Game.Client.Systems.ClientAssets.Model.Mesh;

using EventHorizon.Game.Client.Engine.Entity.Api;
using EventHorizon.Game.Client.Systems.ClientAssets.Api;

public class ClientAssetMeshDetails : ClientAssetDetails
{
    public string AssetInstanceId { get; }
    public ClientAsset ClientAsset { get; }
    public IVector3 Position { get; }

    public bool SkipCaching { get; }

    public ClientAssetMeshDetails(
        string assetInstanceId,
        ClientAsset clientAsset,
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
        ClientAsset clientAsset,
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
