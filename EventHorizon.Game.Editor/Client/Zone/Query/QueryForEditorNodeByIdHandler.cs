namespace EventHorizon.Game.Editor.Client.Zone.Query
{
    using System.Threading;
    using System.Threading.Tasks;

    using EventHorizon.Game.Client.Core.Command.Model;
    using EventHorizon.Game.Editor.Client.Zone.Model;
    using EventHorizon.Game.Editor.Client.Zone.Reload;
    using EventHorizon.Game.Editor.Zone.Editor.Services.Model;

    using MediatR;

    public class QueryForEditorNodeByIdHandler
        : IRequestHandler<QueryForEditorNodeById, CommandResult<EditorNode>>
    {
        private readonly IMediator _mediator;

        public QueryForEditorNodeByIdHandler(
            IMediator mediator
        )
        {
            _mediator = mediator;
        }

        public async Task<CommandResult<EditorNode>> Handle(
            QueryForEditorNodeById request,
            CancellationToken cancellationToken
        )
        {
            var zoneStateResult = await _mediator.Send(
                new QueryForActiveZone(),
                cancellationToken
            );
            if (!zoneStateResult)
            {
                return zoneStateResult.ErrorCode;
            }

            var zoneState = zoneStateResult.Result;
            if (zoneState.IsPendingReload)
            {
                return ZoneClientEditorErrorCodes.ZONE_STATE_PENDING_RELOAD;
            }
            else if (zoneState.IsLoading)
            {
                return ZoneClientEditorErrorCodes.ZONE_STATE_IS_LOADING;
            }

            var editorNode = zoneState.EditorState.GetNode(
                request.Id
            );
            if (editorNode.IsNull())
            {
                await _mediator.Send(
                    new ReloadPendingZoneStateCommand(),
                    cancellationToken
                );

                return ZoneClientEditorErrorCodes.EDITOR_NODE_NOT_FOUND;
            }

            return editorNode;
        }
    }
}
