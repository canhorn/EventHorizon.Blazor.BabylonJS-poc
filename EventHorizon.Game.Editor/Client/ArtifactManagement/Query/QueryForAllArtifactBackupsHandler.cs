namespace EventHorizon.Game.Editor.Client.ArtifactManagement.Query;

using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using EventHorizon.Game.Client.Core.Command.Model;
using EventHorizon.Game.Editor.Client.ArtifactManagement.Components.Model;
using EventHorizon.Game.Editor.Model;
using EventHorizon.Game.Server.Asset.Query;
using MediatR;

public class QueryForAllArtifactBackupsHandler
    : IRequestHandler<QueryForAllArtifactBackups, CommandResult<IEnumerable<ArtifactViewModel>>>
{
    private readonly ISender _sender;
    private readonly GamePlatformServiceSettings _settings;

    public QueryForAllArtifactBackupsHandler(ISender sender, GamePlatformServiceSettings settings)
    {
        _sender = sender;
        _settings = settings;
    }

    public async Task<CommandResult<IEnumerable<ArtifactViewModel>>> Handle(
        QueryForAllArtifactBackups request,
        CancellationToken cancellationToken
    )
    {
        var result = await _sender.Send(new QueryForAssetServerArtifacts(), cancellationToken);
        if (!result)
        {
            return result.ErrorCode;
        }

        return new(
            result
                .Result.BackupList.OrderBy(backup => backup.Path)
                // TODO: Filter by Service
                .Reverse()
                .Select(backup => new ArtifactViewModel
                {
                    Service = backup.Service,
                    ReferenceId = backup.ReferenceId,
                    CreatedDate = backup.Created,
                    Path = $"{_settings.AssetServer}{backup.Path}",
                })
        );
    }
}
