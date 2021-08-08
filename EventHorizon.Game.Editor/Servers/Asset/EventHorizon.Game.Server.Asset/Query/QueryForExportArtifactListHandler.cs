namespace EventHorizon.Game.Server.Asset.Query
{
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using EventHorizon.Game.Client.Core.Command.Model;
    using EventHorizon.Game.Server.Asset.Api;
    using EventHorizon.Game.Server.Asset.Model;
    using MediatR;

    public class QueryForExportArtifactListHandler
        : IRequestHandler<QueryForExportArtifactList, CommandResult<IEnumerable<ExportArtifact>>>
    {
        private readonly AssetServerAdminService _service;

        public QueryForExportArtifactListHandler(
            AssetServerAdminService service
        )
        {
            _service = service;
        }

        public async Task<CommandResult<IEnumerable<ExportArtifact>>> Handle(
            QueryForExportArtifactList request,
            CancellationToken cancellationToken
        )
        {
            var result = await _service.ExportApi.ArtifactList(
                cancellationToken
            );
            if (!result.Success
                || result.Result.IsNull()
            )
            {
                return new CommandResult<IEnumerable<ExportArtifact>>(
                    result.ErrorCode ?? AssetServerAdminErrorCodes.BAD_API_REQUEST
                );
            }

            return new CommandResult<IEnumerable<ExportArtifact>>(
                result.Result
            );
        }
    }
}
