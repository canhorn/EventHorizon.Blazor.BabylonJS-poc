namespace EventHorizon.Game.Client.Systems.Player.Modules.Input.Model
{
    using System;
    using System.Threading.Tasks;
    using EventHorizon.Game.Client.Core.Factory.Api;
    using EventHorizon.Game.Client.Core.Timer.Api;
    using EventHorizon.Game.Client.Engine.Input.Api;
    using EventHorizon.Game.Client.Engine.Input.Register;
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
                100,
                HandleMovePlayer
            );
            _movePlayerIntervalTimer.Start();
        }

        public override Task Dispose()
        {
            _movePlayerIntervalTimer.Dispose();
            return Task.CompletedTask;
        }

        public override Task Update()
        {
            return Task.CompletedTask;
        }

        private async Task InitKeybaord()
        {
            await _mediator.Send(
                new RegisterInputCommand(
                    new InputOptions(
                        "f",
                        pressed: _ => { },
                        released: _ =>
                        {
                            // TODO: INTERACTION_MODULE_NAME 
                            // publish RunInteractionEvent
                        }
                    )
                )
            );
            await _mediator.Send(
                new RegisterInputCommand(
                    new InputOptions(
                        "w",
                        pressed: _ =>
                        {
                            _moveDirection = "FORWARD";
                        },
                        released: _ =>
                        {
                            _moveDirection = "STOP";
                        }
                    )
                )
            );
            await _mediator.Send(
                new RegisterInputCommand(
                    new InputOptions(
                        "a",
                        pressed: _ =>
                        {
                            _moveDirection = "LEFT";
                        },
                        released: _ =>
                        {
                            _moveDirection = "STOP";
                        }
                    )
                )
            );
            await _mediator.Send(
                new RegisterInputCommand(
                    new InputOptions(
                        "d",
                        pressed: _ =>
                        {
                            _moveDirection = "RIGHT";
                        },
                        released: _ =>
                        {
                            _moveDirection = "STOP";
                        }
                    )
                )
            );
            await _mediator.Send(
                new RegisterInputCommand(
                    new InputOptions(
                        "s",
                        pressed: _ =>
                        {
                            _moveDirection = "BACKWARDS";
                        },
                        released: _ =>
                        {
                            _moveDirection = "STOP";
                        }
                    )
                )
            );
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
