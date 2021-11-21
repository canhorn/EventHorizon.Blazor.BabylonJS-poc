namespace EventHorizon.Game.Server.Asset.Query;

using System.Threading;
using System.Threading.Tasks;

using EventHorizon.Game.Client.Core.Command.Model;
using EventHorizon.Game.Server.Asset.Api;
using EventHorizon.Game.Server.Asset.Model;

using MediatR;

public class QueryForAssetServerArtifactsHandler
    : IRequestHandler<QueryForAssetServerArtifacts, CommandResult<AssetServerArtifacts>>
{
    private readonly AssetServerAdminService _service;

    public QueryForAssetServerArtifactsHandler(
        AssetServerAdminService service
    )
    {
        _service = service;
    }

    public async Task<CommandResult<AssetServerArtifacts>> Handle(
        QueryForAssetServerArtifacts request,
        CancellationToken cancellationToken
    )
    {
        var result = await _service.CommonApi.ArtifactList(
            cancellationToken
        );
        if (!result.Success
            || result.Result.IsNull()
        )
        {
            return new CommandResult<AssetServerArtifacts>(
                result.ErrorCode ?? AssetServerAdminErrorCodes.BAD_API_REQUEST
            );
        }

        return new CommandResult<AssetServerArtifacts>(
            MapResult(
                result.Result
            )
        );
    }
    private static AssetServerArtifacts MapResult(
        ArtifactListResult result
    ) => new(
        result.ExportList,
        result.ImportList,
        result.BackupList
    );
}
