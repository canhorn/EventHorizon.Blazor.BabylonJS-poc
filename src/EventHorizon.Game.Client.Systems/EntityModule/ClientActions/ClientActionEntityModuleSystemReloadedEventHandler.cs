namespace EventHorizon.Game.Client.Systems.EntityModule.ClientActions;

using System.Threading;
using System.Threading.Tasks;

using EventHorizon.Game.Client.Systems.Entity.Dispose;
using EventHorizon.Game.Client.Systems.EntityModule.Api;
using EventHorizon.Game.Client.Systems.EntityModule.Register;

using MediatR;

public class ClientActionEntityModuleSystemReloadedEventHandler
    : INotificationHandler<ClientActionEntityModuleSystemReloadedEvent>
{
    private readonly IPublisher _publisher;
    private readonly EntityBaseScriptModuleState _baseScriptModuleState;
    private readonly EntityPlayerScriptModuleState _playerScriptModuleState;

    public ClientActionEntityModuleSystemReloadedEventHandler(
        IPublisher publisher,
        EntityBaseScriptModuleState baseScriptModuleState,
        EntityPlayerScriptModuleState playerScriptModuleState
    )
    {
        _publisher = publisher;
        _baseScriptModuleState = baseScriptModuleState;
        _playerScriptModuleState = playerScriptModuleState;
    }

    public async Task Handle(
        ClientActionEntityModuleSystemReloadedEvent notification,
        CancellationToken cancellationToken
    )
    {
        foreach (var baseModule in _baseScriptModuleState.All())
        {
            await _publisher.Publish(
                new DisposeOfAndRemoveRegisteredEntityModuleEvent(
                    baseModule.Name
                ),
                cancellationToken
            );
        }
        foreach (var baseModule in _playerScriptModuleState.All())
        {
            await _publisher.Publish(
                new DisposeOfAndRemoveRegisteredEntityModuleEvent(
                    baseModule.Name
                ),
                cancellationToken
            );
        }

        _baseScriptModuleState.Reset();
        _playerScriptModuleState.Reset();

        foreach (var baseModule in notification.BaseEntityScriptModuleList)
        {
            _baseScriptModuleState.Set(
                baseModule
            );
        }
        foreach (var playerModule in notification.PlayerEntityScriptModuleList)
        {
            _playerScriptModuleState.Set(
                playerModule
            );
        }

        await _publisher.Publish(
            new BaseEntityModulesChangedEvent(),
            cancellationToken
        );
        await _publisher.Publish(
            new PlayerEntityModulesChangedEvent(),
            cancellationToken
        );
    }
}
