namespace EventHorizon.Game.Editor.Zone.Editor.Services.Query
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using EventHorizon.Game.Client.Core.Command.Model;
    using EventHorizon.Game.Editor.Zone.Editor.Services.Api;
    using EventHorizon.Game.Editor.Zone.Editor.Services.Model;
    using MediatR;

    public class QueryForActiveEditorNodeListHandler
        : IRequestHandler<QueryForActiveEditorNodeList, CommandResult<EditorNodeList>>
    {
        private readonly ZoneEditorServices _zoneEditorServices;

        public QueryForActiveEditorNodeListHandler(
            ZoneEditorServices zoneEditorServices
        )
        {
            _zoneEditorServices = zoneEditorServices;
        }

        public async Task<CommandResult<EditorNodeList>> Handle(
            QueryForActiveEditorNodeList request,
            CancellationToken cancellationToken
        )
        {
            // TODO: pull from cache the ZoneEditorState.EditorNodeList
            return new CommandResult<EditorNodeList>(
                await _zoneEditorServices.Api.GetEditorZoneList()
            );
        }
    }
}
