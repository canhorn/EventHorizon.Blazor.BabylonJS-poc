namespace EventHorizon.Game.Editor.Client.Zone.Get
{
    using System.Threading;
    using System.Threading.Tasks;

    using EventHorizon.Game.Client.Core.Command.Model;
    using EventHorizon.Game.Editor.Client.Authentication.Query;
    using EventHorizon.Game.Editor.Client.Zone.Api;
    using EventHorizon.Game.Editor.Client.Zone.Model;
    using EventHorizon.Game.Editor.Zone.Editor.Services.Connect;
    using EventHorizon.Game.Editor.Zone.Editor.Services.Query;
    using EventHorizon.Game.Editor.Zone.Services.Connect;
    using EventHorizon.Game.Editor.Zone.Services.Query;

    using MediatR;

    public class GetZoneStateCommandHandler
        : IRequestHandler<GetZoneStateCommand, CommandResult<ZoneState>>
    {
        private readonly IMediator _mediator;

        public GetZoneStateCommandHandler(
            IMediator mediator
        )
        {
            _mediator = mediator;
        }

        public async Task<CommandResult<ZoneState>> Handle(
            GetZoneStateCommand request,
            CancellationToken cancellationToken
        )
        {
            var accessTokenResult = await _mediator.Send(
                new QueryForCurrentAccessToken(),
                cancellationToken
            );
            if (!accessTokenResult.Success)
            {
                return new CommandResult<ZoneState>(
                    "Not Authorized"
                );
            }
            var accessToken = accessTokenResult.Result;
            var zoneDetails = request.ZoneDetails;
            var zoneState = new ZoneStateModel
            {
                Zone = zoneDetails
            };
            // TODO: Set/Persist the Current CoreZoneDetails
            // This should make it so the page can be refreshed and the zone will be reconnected.
            // Save to LocalStorage?

            // Start Connection to Zone Server for CoreZoneDetails
            var connectionStartResult = await _mediator.Send(
                new StartConnectionToZoneServerCommand(
                    accessToken,
                    zoneDetails
                ),
                cancellationToken
            );
            if (!connectionStartResult.Success)
            {
                return new CommandResult<ZoneState>(
                    connectionStartResult.ErrorCode
                );
            }

            // Get the Current ZoneInfo
            var zoneInfoResult = await _mediator.Send(
                new QueryForZoneInfo(),
                cancellationToken
            );
            if (!zoneInfoResult.Success)
            {
                return new CommandResult<ZoneState>(
                    zoneInfoResult.ErrorCode
                );
            }
            zoneState.ZoneInfo = zoneInfoResult.Result;

            // Start Connection to ZoneEditor Server for ZoneCoreDetails
            var editorResult = await _mediator.Send(
                new StartConnectionToZoneEditorServerCommand(
                    accessToken,
                    zoneDetails
                ),
                cancellationToken
            );
            if (!editorResult.Success)
            {
                return new CommandResult<ZoneState>(
                    editorResult.ErrorCode
                );
            }
            //  Get the ZoneEditorState for the current ZoneDetails
            var zoneEditorState = await _mediator.Send(
                new QueryForZoneEditorState(),
                cancellationToken
            );
            if (!zoneEditorState.Success)
            {
                return new CommandResult<ZoneState>(
                    zoneEditorState.ErrorCode
                );
            }
            zoneState.EditorState = zoneEditorState.Result;
            zoneState.IsLoading = false;
            zoneState.IsPendingReload = false;

            return new CommandResult<ZoneState>(
                zoneState
            );
        }
    }
}
