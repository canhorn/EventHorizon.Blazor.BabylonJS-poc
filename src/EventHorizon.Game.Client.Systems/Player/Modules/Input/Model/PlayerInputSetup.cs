namespace EventHorizon.Game.Client.Systems.Player.Modules.Input.Model;

using System.Threading.Tasks;
using EventHorizon.Game.Client.Engine.Input.Api;
using EventHorizon.Game.Client.Engine.Input.Model;
using EventHorizon.Game.Client.Engine.Systems.Camera.Set;
using EventHorizon.Game.Client.Systems.Entity.Modules.InteractionIndicator.Run;
using EventHorizon.Game.Client.Systems.Player.Modules.Input.Api;
using EventHorizon.Game.Client.Systems.Player.Modules.Input.Move;
using MediatR;

public interface PlayerInputSetup
{
    Task Setup(InputModule module, PlayerInputConfig config);
}

public class StandardPlayerInputSetup(IMediator mediator) : PlayerInputSetup
{
    public async Task Setup(InputModule module, PlayerInputConfig config)
    {
        foreach (var (_, keyInput) in config.KeyInputMap)
        {
            switch (keyInput.Type)
            {
                case "PlayerMove":
                    await SetupPlayerMoveInput(module, keyInput);
                    break;
                case "SetActiveCamera":
                    await SetupActiveCameraInput(module, keyInput);
                    break;
                case "RunInteraction":
                    await SetupRunInteractionInput(module, keyInput);
                    break;
                default:
                    break;
            }
        }
    }

    private async Task SetupPlayerMoveInput(
        InputModule module,
        PlayerInputConfig.PlayerKeyInput keyInput
    )
    {
        var pressedDirection = new Option<MoveDirection>();
        var releasedDirection = new Option<MoveDirection>();
        // TODO: Test to see if can go direct to MoveDirection
        if (keyInput.TryGet<int>("Pressed", out var pressedResult))
        {
            pressedDirection = new Option<MoveDirection>(
                (MoveDirection)pressedResult
            );
        }
        if (keyInput.TryGet<int>("Released", out var releasedResult))
        {
            releasedDirection = new Option<MoveDirection>(
                (MoveDirection)releasedResult
            );
        }

        await module.RegisterInput(
            new InputOptions(
                keyInput.Key,
                pressed: _ =>
                {
                    if (pressedDirection.HasValue)
                    {
                        mediator.Publish(
                            new MovePlayerInDirectionEvent(
                                pressedDirection.Value
                            )
                        );
                    }

                    return Task.CompletedTask;
                },
                released: _ =>
                {
                    if (releasedDirection.HasValue)
                    {
                        mediator.Publish(
                            new MovePlayerInDirectionEvent(
                                releasedDirection.Value
                            )
                        );
                    }

                    return Task.CompletedTask;
                }
            )
        );
    }

    private async Task SetupActiveCameraInput(
        InputModule module,
        PlayerInputConfig.PlayerKeyInput keyInput
    )
    {
        var cameraName = keyInput.Get<string>("Camera");
        if (cameraName.HasValue.IsNotTrue())
        {
            return;
        }

        await module.RegisterInput(
            new InputOptions(
                keyInput.Key,
                pressed: _ => Task.CompletedTask,
                released: async _ =>
                {
                    await mediator.Send(
                        new SetActiveCameraCommand(cameraName.Value ?? string.Empty)
                    );
                }
            )
        );
    }

    private async Task SetupRunInteractionInput(
        InputModule module,
        PlayerInputConfig.PlayerKeyInput keyInput
    )
    {
        await module.RegisterInput(
            new InputOptions(
                keyInput.Key,
                pressed: _ => Task.CompletedTask,
                released: async _ =>
                {
                    await mediator.Publish(new RunInteractionEvent());
                }
            )
        );
    }
}
