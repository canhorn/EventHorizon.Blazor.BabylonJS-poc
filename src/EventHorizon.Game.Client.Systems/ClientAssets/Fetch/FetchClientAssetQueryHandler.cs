namespace EventHorizon.Game.Client.Systems.ClientAssets.Fetch;

using System.Threading;
using System.Threading.Tasks;

using EventHorizon.Game.Client.Core.Query.Model;
using EventHorizon.Game.Client.Systems.ClientAssets.Api;

using MediatR;

public class FetchClientAssetQueryHandler
    : IRequestHandler<FetchClientAssetQuery, QueryResult<ClientAsset>>
{
    private readonly ClientAssetState _clientAssetStore;

    public FetchClientAssetQueryHandler(ClientAssetState clientAssetStore)
    {
        _clientAssetStore = clientAssetStore;
    }

    public Task<QueryResult<ClientAsset>> Handle(
        FetchClientAssetQuery request,
        CancellationToken cancellationToken
    )
    {
        var clientAsset = _clientAssetStore.Get(request.AssetId);

        if (clientAsset.HasValue)
        {
            return new QueryResult<ClientAsset>(clientAsset.Value).FromResult();
        }

        return new QueryResult<ClientAsset>("not_found").FromResult();
    }
}
