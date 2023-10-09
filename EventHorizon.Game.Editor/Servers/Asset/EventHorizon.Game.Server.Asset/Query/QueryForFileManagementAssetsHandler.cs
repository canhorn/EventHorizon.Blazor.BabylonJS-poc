namespace EventHorizon.Game.Server.Asset.Query;

using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

using EventHorizon.Game.Client.Core.Command.Model;
using EventHorizon.Game.Server.Asset.Api;
using EventHorizon.Game.Server.Asset.Model;

using MediatR;

public class QueryForFileManagementAssetsHandler
    : IRequestHandler<
        QueryForFileManagementAssets,
        CommandResult<FileManagementAssets>
    >
{
    private readonly AssetServerAdminService _service;

    public QueryForFileManagementAssetsHandler(AssetServerAdminService service)
    {
        _service = service;
    }

    public async Task<CommandResult<FileManagementAssets>> Handle(
        QueryForFileManagementAssets request,
        CancellationToken cancellationToken
    )
    {
        var result = await _service.FileManagementApi.Assets(cancellationToken);
        if (!result.Success || result.Result.IsNull())
        {
            return new CommandResult<FileManagementAssets>(
                result.ErrorCode ?? AssetServerAdminErrorCodes.BAD_API_REQUEST
            );
        }

        return new CommandResult<FileManagementAssets>(result.Result);
    }
}
