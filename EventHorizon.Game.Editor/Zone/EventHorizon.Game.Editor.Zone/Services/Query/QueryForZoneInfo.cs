namespace EventHorizon.Game.Editor.Zone.Services.Query;

using EventHorizon.Game.Client.Core.Command.Model;
using EventHorizon.Game.Editor.Zone.Services.Model;

using MediatR;

public struct QueryForZoneInfo : IRequest<CommandResult<ZoneInfo>> { }
