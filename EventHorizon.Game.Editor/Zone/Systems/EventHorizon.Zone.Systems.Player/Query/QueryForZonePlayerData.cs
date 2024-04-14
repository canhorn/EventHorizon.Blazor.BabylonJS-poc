namespace EventHorizon.Zone.Systems.Player.Query;

using EventHorizon.Game.Client.Core.Command.Model;
using EventHorizon.Zone.Systems.Player.Model;
using MediatR;

public record QueryForZonePlayerData() : IRequest<CommandResult<PlayerObjectEntityDataModel>>;
