namespace EventHorizon.Game.Editor.Zone.Services.Api;

using System.Threading;
using System.Threading.Tasks;
using EventHorizon.Game.Editor.Zone.Services.Model;
using EventHorizon.Zone.System.Server.Scripts.Model;

public interface ZoneAdminServerScriptsApi
{
    public Task<ApiResponse<ServerScriptsErrorDetailsResponse>> GetErrorDetails(
        CancellationToken cancellationToken
    );
}
