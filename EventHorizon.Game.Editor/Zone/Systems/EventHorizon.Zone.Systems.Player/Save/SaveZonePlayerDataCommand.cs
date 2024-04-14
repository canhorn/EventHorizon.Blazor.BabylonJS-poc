namespace EventHorizon.Zone.Systems.Player.Save;

using EventHorizon.Game.Client.Core.Command.Model;
using EventHorizon.Zone.Systems.Player.Model;
using MediatR;

public record SaveZonePlayerDataCommand(PlayerObjectEntityDataModel PlayerData)
    : IRequest<CommandResult<PlayerObjectEntityDataModel>>;
