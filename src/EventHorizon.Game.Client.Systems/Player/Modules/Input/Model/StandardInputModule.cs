namespace EventHorizon.Game.Client.Systems.Player.Modules.Input.Model;

using System.Collections.Generic;
using System.Threading.Tasks;

using EventHorizon.Game.Client.Core.Factory.Api;
using EventHorizon.Game.Client.Core.Timer.Api;
using EventHorizon.Game.Client.Engine.Input.Api;
using EventHorizon.Game.Client.Engine.Input.Register;
using EventHorizon.Game.Client.Engine.Input.Unregister;
using EventHorizon.Game.Client.Engine.Systems.Entity.Api;
using EventHorizon.Game.Client.Engine.Systems.Module.Model;
using EventHorizon.Game.Client.Systems.Player.Action.Model;
using EventHorizon.Game.Client.Systems.Player.Action.Model.Send;
using EventHorizon.Game.Client.Systems.Player.Api;
using EventHorizon.Game.Client.Systems.Player.ClientAction;
using EventHorizon.Game.Client.Systems.Player.Modules.Input.Api;
using EventHorizon.Game.Client.Systems.Player.Modules.Input.Move;

using MediatR;

using Microsoft.Extensions.Logging;

public class StandardInputModule
    : ModuleEntityBase,
        InputModule,
        ClientActionPlayerSystemReloadedEventObserver,
        MovePlayerInDirectionEventObserver
{
    private readonly ILogger _logger = GameServiceProvider.GetService<
        ILogger<StandardInputModule>
    >();
    private readonly IMediator _mediator =
        GameServiceProvider.GetService<IMediator>();
    private readonly IIntervalTimerService _movePlayerIntervalTimer =
        GameServiceProvider
            .GetService<IFactory<IIntervalTimerService>>()
            .Create();
    private readonly PlayerInputSetup _playerInputSetup =
        GameServiceProvider.GetService<PlayerInputSetup>();
    private readonly IList<string> _registeredInput = new List<string>();
    private readonly IPlayerEntity _entity;

    private PlayerInputConfig _inputConfig = new();
    private MoveDirection _moveDirection;

    public override int Priority => 0;

    public StandardInputModule(IPlayerEntity playerEntity)
    {
        _entity = playerEntity;
        _moveDirection = MoveDirection.Stationary;

        var playerConfiguration =
            _entity.GetPropertyAsOption<ObjectEntityConfiguration>(
                "playerConfiguration"
            );
        if (playerConfiguration.HasValue.IsNotTrue())
        {
            _logger.LogDebug("Player Configuration not Found.");
            return;
        }
        SetupPlayerInputConfig(playerConfiguration.Value);
    }

    public override async Task Initialize()
    {
        await _playerInputSetup.Setup(this, _inputConfig);
        _movePlayerIntervalTimer.Setup(
            _inputConfig.MovementDelay,
            HandleMovePlayer
        );
        _movePlayerIntervalTimer.Start();

        GamePlatfrom.RegisterObserver(this);
    }

    public override async Task Dispose()
    {
        GamePlatfrom.UnRegisterObserver(this);

        _movePlayerIntervalTimer.Dispose();
        foreach (var registeredInput in _registeredInput)
        {
            await UnRegisterInput(registeredInput);
        }
    }

    public override Task Update()
    {
        return Task.CompletedTask;
    }

    public async Task<Option<string>> RegisterInput(InputOptions options)
    {
        var result = await _mediator.Send(new RegisterInputCommand(options));
        if (result.Success)
        {
            _registeredInput.Add(result.Result);
            return result.Result.ToOption();
        }
        return new Option<string>(null);
    }

    public async Task UnRegisterInput(string inputHandler)
    {
        await _mediator.Send(new UnregisterInputCommand(inputHandler));
    }

    public async Task ResetToDefaultLayout()
    {
        await Dispose();
        await Initialize();
    }

    public async Task Handle(ClientActionPlayerSystemReloadedEvent args)
    {
        if (SetupPlayerInputConfig(args.PlayerConfiguration))
        {
            await ResetToDefaultLayout();
        }
    }

    public Task Handle(MovePlayerInDirectionEvent args)
    {
        _moveDirection = args.Direction;

        return Task.CompletedTask;
    }

    private bool SetupPlayerInputConfig(ObjectEntityConfiguration config)
    {
        var inputConfig = config.Get<PlayerInputConfig>(
            PlayerInputConfig.PROPERTY_NAME
        );

        if (inputConfig.HasValue.IsNotTrue())
        {
            _logger.LogDebug("Failed to Load Player Input Configuration.");
            return false;
        }
        _inputConfig = inputConfig.Value;

        return true;
    }

    private async Task HandleMovePlayer()
    {
        if (_moveDirection == MoveDirection.Stationary)
        {
            return;
        }
        else if (_moveDirection == MoveDirection.Stop)
        {
            await _mediator.Publish(
                new InvokePlayerActionEvent(PlayerActions.STOP)
            );
            return;
        }

        await _mediator.Publish(
            new InvokePlayerActionEvent(
                PlayerActions.MOVE,
                new PlayerMoveDirectionActionData(_moveDirection)
            )
        );
        if (_inputConfig.StopMovementOnTick)
        {
            _moveDirection = MoveDirection.Stop;
        }
    }
}
