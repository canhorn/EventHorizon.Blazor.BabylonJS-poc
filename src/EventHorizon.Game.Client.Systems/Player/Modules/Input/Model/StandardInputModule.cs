namespace EventHorizon.Game.Client.Systems.Player.Modules.Input.Model
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using EventHorizon.Game.Client.Core.Factory.Api;
    using EventHorizon.Game.Client.Core.Timer.Api;
    using EventHorizon.Game.Client.Engine.Input.Api;
    using EventHorizon.Game.Client.Engine.Input.Register;
    using EventHorizon.Game.Client.Engine.Input.Unregister;
    using EventHorizon.Game.Client.Engine.Systems.Camera.Set;
    using EventHorizon.Game.Client.Engine.Systems.Module.Model;
    using EventHorizon.Game.Client.Systems.Entity.Modules.InteractionIndicator.Run;
    using EventHorizon.Game.Client.Systems.Player.Action.Model;
    using EventHorizon.Game.Client.Systems.Player.Action.Model.Send;
    using EventHorizon.Game.Client.Systems.Player.Api;
    using EventHorizon.Game.Client.Systems.Player.Modules.Input.Api;
    using MediatR;

    public class StandardInputModule
        : ModuleEntityBase,
        InputModule
    {
        private readonly IMediator _mediator = GameServiceProvider.GetService<IMediator>();
        private readonly IIntervalTimerService _movePlayerIntervalTimer = GameServiceProvider.GetService<IFactory<IIntervalTimerService>>().Create();
        private readonly IList<string> _registeredInput = new List<string>();
        private readonly IPlayerEntity _entity;

        private string _moveDirection;

        public override int Priority => 0;

        public StandardInputModule(
            IPlayerEntity playerEntity
        )
        {
            _entity = playerEntity;
            _moveDirection = "__init__";
        }

        public override async Task Initialize()
        {
            await InitKeyboard();
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
                await UnRegisterInput(
                    registeredInput
                );
            }
        }

        public override Task Update()
        {
            return Task.CompletedTask;
        }

        private async Task InitKeyboard()
        {
            // TODO: [Input] - Implement way to Load this from some where.
            await RegisterInput(
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
            await RegisterInput(
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
            await RegisterInput(
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
                        return _mediator.Publish(
                            new RunInteractionEvent()
                        );
                    }
                )
            );
            await RegisterInput(
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
            await RegisterInput(
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
            await RegisterInput(
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
            await RegisterInput(
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

        public async Task<Option<string>> RegisterInput(
            InputOptions options
        )
        {
            var result = await _mediator.Send(
                new RegisterInputCommand(
                    options
                )
            );
            if (result.Success)
            {
                _registeredInput.Add(
                    result.Result
                );
                return result.Result.ToOption();
            }
            return new Option<string>(
                null
            );
        }

        public async Task UnRegisterInput(
            string inputHandler
        )
        {
            await _mediator.Send(
                new UnregisterInputCommand(
                    inputHandler
                )
            );
        }

        public async Task ResetToDefaultLayout()
        {
            await Dispose();
            await Initialize();
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
