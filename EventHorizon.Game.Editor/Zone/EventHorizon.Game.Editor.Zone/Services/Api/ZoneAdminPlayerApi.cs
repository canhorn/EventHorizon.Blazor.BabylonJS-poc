namespace EventHorizon.Game.Editor.Zone.Services.Api;

using System.Threading;
using System.Threading.Tasks;
using EventHorizon.Game.Editor.Zone.Services.Model;
using EventHorizon.Zone.Systems.Player.Model;

public interface ZoneAdminPlayerApi
{
    Task<ApiResponse<PlayerObjectEntityDataModel>> GetData(
        CancellationToken cancellationToken
    );
    Task<StandardApiResponse> SaveData(
        PlayerObjectEntityDataModel playerData,
        CancellationToken cancellationToken
    );
}
