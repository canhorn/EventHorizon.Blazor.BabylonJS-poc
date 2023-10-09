namespace EventHorizon.Game.Editor.Zone.Editor.Services.Query;

using EventHorizon.Game.Client.Core.Command.Model;
using EventHorizon.Game.Editor.Zone.Editor.Services.Model;

using MediatR;

public struct QueryForActiveEditorNodeList
    : IRequest<CommandResult<EditorNodeList>> { }
