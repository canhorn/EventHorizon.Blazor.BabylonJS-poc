namespace EventHorizon.Game.Editor.Zone.Services.Api
{
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using EventHorizon.Game.Editor.Zone.Services.Model;
    using EventHorizon.Zone.Systems.ClientAssets.Model;

    public interface ZoneAdminClientAssetsApi
    {
        public Task<ApiResponse<List<ClientAsset>>> All(
            CancellationToken cancellationToken
        );

        public Task<ApiResponse<ClientAsset>> Get(
            string id,
            CancellationToken cancellationToken
        );

        public Task<StandardApiResponse> Create(
            ClientAsset clientAsset,
            CancellationToken cancellationToken
        );

        public Task<StandardApiResponse> Update(
            ClientAsset clientAsset,
            CancellationToken cancellationToken
        );

        public Task<StandardApiResponse> Delete(
            string id,
            CancellationToken cancellationToken
        );
    }
}
