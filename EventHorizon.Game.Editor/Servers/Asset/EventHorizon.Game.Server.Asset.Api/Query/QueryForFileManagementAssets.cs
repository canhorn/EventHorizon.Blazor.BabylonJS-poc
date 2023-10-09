namespace EventHorizon.Game.Server.Asset.Query;

using EventHorizon.Cache;
using EventHorizon.Game.Client.Core.Command.Model;
using EventHorizon.Game.Server.Asset.Model;

using MediatR;

[GenerateCache]
public struct QueryForFileManagementAssets
    : IRequest<CommandResult<FileManagementAssets>>,
        CacheKey
{
    public readonly string CacheKeyPrefix => "asset-server";
    public readonly string CacheKey =>
        $"{CacheKeyPrefix}:{nameof(QueryForFileManagementAssets)}";
}
