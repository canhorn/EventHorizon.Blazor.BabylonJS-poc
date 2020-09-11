namespace EventHorizon.Game.Client.Systems.ClientAssets.Fetch
{
    using System.Threading;
    using System.Threading.Tasks;
    using EventHorizon.Game.Client.Core.Query.Model;
    using EventHorizon.Game.Client.Systems.ClientAssets.Api;
    using MediatR;

    public class FetchClientAssetQueryHandler
        : IRequestHandler<FetchClientAssetQuery, QueryResult<IClientAsset>>
    {
        private readonly IClientAssetStore _clientAssetStore;

        public FetchClientAssetQueryHandler(
            IClientAssetStore clientAssetStore
        )
        {
            _clientAssetStore = clientAssetStore;
        }

        public Task<QueryResult<IClientAsset>> Handle(
            FetchClientAssetQuery request,
            CancellationToken cancellationToken
        )
        {
            var clientAsset = _clientAssetStore.Get(
                request.AssetId
            );
            
            if (clientAsset.HasValue)
            {
                return new QueryResult<IClientAsset>(
                    clientAsset.Value
                ).FromResult();
            }

            return new QueryResult<IClientAsset>(
                "not_found"
            ).FromResult();
        }
    }
}
