namespace EventHorizon.Game.Editor.Zone.Editor.Services.Query;

using System;
using System.Threading;
using System.Threading.Tasks;

using EventHorizon.Game.Client.Core.Command.Model;
using EventHorizon.Game.Editor.Zone.Editor.Services.Api;
using EventHorizon.Game.Editor.Zone.Editor.Services.Model;

using MediatR;

public class QueryForZoneEditorStateHandler
    : IRequestHandler<QueryForZoneEditorState, CommandResult<ZoneEditorState>>
{
    private readonly ZoneEditorServices _zoneEditorServices;

    public QueryForZoneEditorStateHandler(ZoneEditorServices zoneEditorServices)
    {
        _zoneEditorServices = zoneEditorServices;
    }

    public async Task<CommandResult<ZoneEditorState>> Handle(
        QueryForZoneEditorState request,
        CancellationToken cancellationToken
    )
    {
        // TODO: Use Cache to get State
        var result = await _zoneEditorServices.Api.GetEditorZoneList();
        if (!result.Success)
        {
            return new(
                result.ErrorCode ?? ZoneEditorErrorCodes.EDITOR_API_ERROR
            );
        }
        return new CommandResult<ZoneEditorState>(
            new ZoneEditorStateModel(result.Result)
        );
    }
}
