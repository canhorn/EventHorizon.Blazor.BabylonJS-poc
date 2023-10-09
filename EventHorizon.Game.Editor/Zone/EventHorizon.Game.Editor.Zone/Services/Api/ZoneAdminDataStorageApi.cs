namespace EventHorizon.Game.Editor.Zone.Services.Api;

using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

using EventHorizon.Game.Editor.Zone.Services.Model;

public interface ZoneAdminDataStorageApi
{
    public Task<ApiResponse<Dictionary<string, object>>> All(
        CancellationToken cancellationToken
    );

    public Task<StandardApiResponse> Create(
        string key,
        string type,
        object value,
        CancellationToken cancellationToken
    );

    public Task<StandardApiResponse> Update(
        string key,
        string type,
        object value,
        CancellationToken cancellationToken
    );

    public Task<StandardApiResponse> Delete(
        string key,
        CancellationToken cancellationToken
    );
}
