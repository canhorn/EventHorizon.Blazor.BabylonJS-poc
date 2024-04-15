namespace EventHorizon.Game.Editor.Zone.Systems.Server.Scripts.Query;

using System.Threading;
using System.Threading.Tasks;
using EventHorizon.Game.Client.Core.Command.Model;
using EventHorizon.Game.Editor.Zone.Services.Api;
using EventHorizon.Zone.System.Server.Scripts.Model;
using EventHorizon.Zone.System.Server.Scripts.Query;
using MediatR;

public class QueryForServerScriptsErrorDetailsHandler
    : IRequestHandler<
        QueryForServerScriptsErrorDetails,
        CommandResult<ServerScriptsErrorDetailsResponse>
    >
{
    private readonly ZoneAdminServices _zoneAdminServices;

    public QueryForServerScriptsErrorDetailsHandler(ZoneAdminServices zoneAdminServices)
    {
        _zoneAdminServices = zoneAdminServices;
    }

    public async Task<CommandResult<ServerScriptsErrorDetailsResponse>> Handle(
        QueryForServerScriptsErrorDetails request,
        CancellationToken cancellationToken
    )
    {
        var result = await _zoneAdminServices.Api.ServerScripts.GetErrorDetails(cancellationToken);
        if (result.Success.IsNotTrue() || result.Result.IsNull())
        {
            return result.ErrorCode ?? ZoneAdminErrorCodes.BAD_API_REQUEST;
        }

        return result.Result;
    }
}
