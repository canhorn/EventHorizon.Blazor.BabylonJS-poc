namespace EventHorizon.Game.Server.Asset.Query
{
    using EventHorizon.Game.Client.Core.Command.Model;
    using EventHorizon.Game.Server.Asset.Model;
    using MediatR;

    public struct QueryForExportStatus
        : IRequest<CommandResult<ExportStatus>>
    {
    }
}
