namespace EventHorizon.Game.Client.Systems.ClientAssets.Fetch;

using EventHorizon.Game.Client.Core.Query.Model;
using EventHorizon.Game.Client.Systems.ClientAssets.Api;

using MediatR;

public class FetchClientAssetQuery : IRequest<QueryResult<ClientAsset>>
{
    public string AssetId { get; }

    public FetchClientAssetQuery(string assetId)
    {
        AssetId = assetId;
    }
}
