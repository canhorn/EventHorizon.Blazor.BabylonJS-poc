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

public class QueryForAllArtifactImportsHandler
    : IRequestHandler<QueryForAllArtifactImports, CommandResult<IEnumerable<ArtifactViewModel>>>
{
    private readonly ISender _sender;
    private readonly GamePlatformServiceSettings _settings;

    public QueryForAllArtifactImportsHandler(
        ISender sender,
        GamePlatformServiceSettings settings
    )
    {
        _sender = sender;
        _settings = settings;
    }

    public async Task<CommandResult<IEnumerable<ArtifactViewModel>>> Handle(
        QueryForAllArtifactImports request,
        CancellationToken cancellationToken
    )
    {
        var result = await _sender.Send(
            new QueryForAssetServerArtifacts(),
            cancellationToken
        );

        if (!result)
        {
            return result.ErrorCode;
        }

        return new(
            result.Result.ImportList
                .OrderBy(export => export.Path)
                .Reverse()
                .Select(
                    export =>
                        new ArtifactViewModel
                        {
                            Service = export.Service,
                            ReferenceId = export.ReferenceId,
                            CreatedDate = export.Created,
                            Path = $"{_settings.AssetServer}{export.Path}",
                        }
                )
            );
    }
}
