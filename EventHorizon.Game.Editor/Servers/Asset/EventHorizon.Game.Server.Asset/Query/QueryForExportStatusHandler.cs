namespace EventHorizon.Game.Server.Asset.Query
{
    using System.Threading;
    using System.Threading.Tasks;
    using EventHorizon.Game.Client.Core.Command.Model;
    using EventHorizon.Game.Server.Asset.Api;
    using EventHorizon.Game.Server.Asset.Model;
    using MediatR;

    public class QueryForExportStatusHandler
        : IRequestHandler<QueryForExportStatus, CommandResult<ExportStatus>>
    {
        private readonly AssetServerAdminService _service;

        public QueryForExportStatusHandler(
            AssetServerAdminService service
        )
        {
            _service = service;
        }

        public async Task<CommandResult<ExportStatus>> Handle(
            QueryForExportStatus request,
            CancellationToken cancellationToken
        )
        {
            var result = await _service.ExportApi.Status(
                cancellationToken
            );
            if (!result.Success
                || result.Result.IsNull()
            )
            {
                return new CommandResult<ExportStatus>(
                    result.ErrorCode ?? AssetServerAdminErrorCodes.BAD_API_REQUEST
                );
            }

            return new CommandResult<ExportStatus>(
                result.Result
            );
        }
    }
}
