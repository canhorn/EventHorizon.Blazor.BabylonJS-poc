namespace EventHorizon.Game.Client.Systems.Player.Modules.Input.Model
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using EventHorizon.Game.Client.Core.Command.Model;
    using EventHorizon.Game.Client.Core.Factory.Api;
    using EventHorizon.Game.Client.Core.Timer.Api;
    using EventHorizon.Game.Client.Engine.Input.Api;
    using EventHorizon.Game.Client.Engine.Input.Register;
    using EventHorizon.Game.Client.Engine.Input.Unregister;
    using EventHorizon.Game.Client.Engine.Systems.Camera.Set;
    using EventHorizon.Game.Client.Engine.Systems.Module.Model;
    using EventHorizon.Game.Client.Systems.Player.Action.Model;
    using EventHorizon.Game.Client.Systems.Player.Action.Model.Send;
    using EventHorizon.Game.Client.Systems.Player.Api;
    using EventHorizon.Game.Client.Systems.Player.Modules.Input.Api;
    using MediatR;

    public class InputModule
        : ModuleEntityBase,
        IInputModule
    {
        private readonly IMediator _mediator;
        private readonly IIntervalTimerService _movePlayerIntervalTimer;
        private readonly IPlayerEntity _entity;
        private string _moveDirection;
        private IList<string> _registeredInput = new List<string>();

        public override int Priority => 0;

        public InputModule(
            IPlayerEntity entity
        )
        {
            _mediator = GameServiceProvider.GetService<IMediator>();
            _movePlayerIntervalTimer = GameServiceProvider.GetService<IFactory<IIntervalTimerService>>().Create();

            _entity = entity;
            _moveDirection = "__init__";
        }

        public override async Task Initialize()
        {
            await InitKeybaord();
            _movePlayerIntervalTimer.Setup(
                50,
                HandleMovePlayer
            );
            _movePlayerIntervalTimer.Start();
        }

        public override async Task Dispose()
        {
            _movePlayerIntervalTimer.Dispose();
            foreach (var registeredInput in _registeredInput)
            {
                await _mediator.Send(
                    new UnregisterInputCommand(
                        registeredInput
                    )
                );
            }
        }

        public override Task Update()
        {
            return Task.CompletedTask;
        }

        private async Task InitKeybaord()
        {
            await TrackInputOption(
                new InputOptions(
                    "1",
                    pressed: _ =>
                    {
                        return Task.CompletedTask;
                    },
                    released: async _ =>
                    {
                        await _mediator.Send(
                            new SetActiveCameraCommand(
                                "player_universal_camera"
                            )
                        );
                    }
                )
            );
            await TrackInputOption(
                new InputOptions(
                    "2",
                    pressed: _ =>
                    {
                        return Task.CompletedTask;
                    },
                    released: async _ =>
                    {
                        await _mediator.Send(
                            new SetActiveCameraCommand(
                                "player_follow_camera"
                            )
                        );
                    }
                )
            );
            await TrackInputOption(
                new InputOptions(
                    "f",
                    pressed: _ =>
                    {
                        return Task.CompletedTask;
                    },
                    released: _ =>
                    {
                        // TODO: INTERACTION_MODULE_NAME 
                        // publish RunInteractionEvent
                        return Task.CompletedTask;
                    }
                )
            );
            await TrackInputOption(
                new InputOptions(
                    "w",
                    pressed: _ =>
                    {
                        _moveDirection = "FORWARD";
                        return Task.CompletedTask;
                    },
                    released: _ =>
                    {
                        _moveDirection = "STOP";
                        return Task.CompletedTask;
                    }
                )
            );
            await TrackInputOption(
                new InputOptions(
                    "a",
                    pressed: _ =>
                    {
                        _moveDirection = "LEFT";
                        return Task.CompletedTask;
                    },
                    released: _ =>
                    {
                        _moveDirection = "STOP";
                        return Task.CompletedTask;
                    }
                )
            );
            await TrackInputOption(
                new InputOptions(
                    "d",
                    pressed: _ =>
                    {
                        _moveDirection = "RIGHT";
                        return Task.CompletedTask;
                    },
                    released: _ =>
                    {
                        _moveDirection = "STOP";
                        return Task.CompletedTask;
                    }
                )
            );
            await TrackInputOption(
                new InputOptions(
                    "s",
                    pressed: _ =>
                    {
                        _moveDirection = "BACKWARDS";
                        return Task.CompletedTask;
                    },
                    released: _ =>
                    {
                        _moveDirection = "STOP";
                        return Task.CompletedTask;
                    }
                )
            );
        }

        private async Task TrackInputOption(
            InputOptions options
        )
        {
            TrackInput(
                await _mediator.Send(
                    new RegisterInputCommand(
                        options
                    )
                )
            );
        }

        private void TrackInput(
            CommandResult<string> commandResult
        )
        {
            if (commandResult.Success)
            {
                _registeredInput.Add(
                    commandResult.Result
                );
            }
        }

        private async Task HandleMovePlayer()
        {
            switch (_moveDirection)
            {
                case "FORWARD":
                    await _mediator.Publish(
                        new InvokePlayerActionEvent(
                            PlayerActions.MOVE,
                            new PlayerMoveDirectionActionData(
                                MoveDirection.Forward
                            )
                        )
                    );
                    break;
                case "BACKWARDS":
                    await _mediator.Publish(
                        new InvokePlayerActionEvent(
                            PlayerActions.MOVE,
                            new PlayerMoveDirectionActionData(
                                MoveDirection.Backwards
                            )
                        )
                    );
                    break;
                case "RIGHT":
                    await _mediator.Publish(
                        new InvokePlayerActionEvent(
                            PlayerActions.MOVE,
                            new PlayerMoveDirectionActionData(
                                MoveDirection.Right
                            )
                        )
                    );
                    break;
                case "LEFT":
                    await _mediator.Publish(
                        new InvokePlayerActionEvent(
                            PlayerActions.MOVE,
                            new PlayerMoveDirectionActionData(
                                MoveDirection.Left
                            )
                        )
                    );
                    break;
                case "STOP":
                    await _mediator.Publish(
                        new InvokePlayerActionEvent(
                            PlayerActions.STOP
                        )
                    );
                    break;
                default:
                    break;
            }
            _moveDirection = "__none__";
        }
    }
}
