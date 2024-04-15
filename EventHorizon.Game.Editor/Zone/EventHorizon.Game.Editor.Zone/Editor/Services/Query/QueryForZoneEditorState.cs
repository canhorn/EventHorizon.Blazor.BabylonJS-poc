namespace EventHorizon.Game.Editor.Zone.Editor.Services.Query;

using EventHorizon.Game.Client.Core.Command.Model;
using EventHorizon.Game.Editor.Zone.Editor.Services.Api;
using MediatR;

public struct QueryForZoneEditorState : IRequest<CommandResult<ZoneEditorState>> { }
