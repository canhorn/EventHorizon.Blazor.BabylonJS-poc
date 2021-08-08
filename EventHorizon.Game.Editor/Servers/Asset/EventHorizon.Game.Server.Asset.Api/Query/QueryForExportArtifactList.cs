namespace EventHorizon.Game.Server.Asset.Query
{
    using System.Collections.Generic;
    using EventHorizon.Game.Client.Core.Command.Model;
    using EventHorizon.Game.Server.Asset.Model;
    using MediatR;

    public struct QueryForExportArtifactList
        : IRequest<CommandResult<IEnumerable<ExportArtifact>>>
    {
    }
}
