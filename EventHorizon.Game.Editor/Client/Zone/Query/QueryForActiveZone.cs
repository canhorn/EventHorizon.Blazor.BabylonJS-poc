namespace EventHorizon.Game.Editor.Client.Zone.Query;

using EventHorizon.Game.Client.Core.Command.Model;
using EventHorizon.Game.Editor.Client.Zone.Api;
using MediatR;

public record QueryForActiveZone : IRequest<CommandResult<ZoneState>> { }
