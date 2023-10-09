namespace EventHorizon.Game.Client.Systems.EntityModule.Info;

using System;
using System.Threading;
using System.Threading.Tasks;

using EventHorizon.Game.Client.Systems.Connection.Zone.Player.Info;
using EventHorizon.Game.Client.Systems.EntityModule.Api;

using MediatR;

public class SetupEntityModulesFromPlayerZoneInfoReceivedEventHandler
    : INotificationHandler<PlayerZoneInfoReceivedEvent>
{
    private readonly EntityBaseScriptModuleState _baseState;
    private readonly EntityPlayerScriptModuleState _playerState;

    public SetupEntityModulesFromPlayerZoneInfoReceivedEventHandler(
        EntityBaseScriptModuleState baseState,
        EntityPlayerScriptModuleState playerState
    )
    {
        _baseState = baseState;
        _playerState = playerState;
    }

    public Task Handle(
        PlayerZoneInfoReceivedEvent notification,
        CancellationToken cancellationToken
    )
    {
        foreach (
            var baseModule in notification
                .PlayerZoneInfo
                .BaseEntityScriptModuleList
        )
        {
            _baseState.Set(baseModule);
        }

        foreach (
            var playerModule in notification
                .PlayerZoneInfo
                .PlayerEntityScriptModuleList
        )
        {
            _playerState.Set(playerModule);
        }

        return Task.CompletedTask;
    }
}
