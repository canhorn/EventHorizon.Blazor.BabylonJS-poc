namespace EventHorizon.Game.Client.Systems.Player.Modules.Camera.Model;

using System.Threading.Tasks;
using EventHorizon.Game.Client.Engine.Systems.Camera.Register;
using EventHorizon.Game.Client.Engine.Systems.Camera.Set;
using EventHorizon.Game.Client.Engine.Systems.Camera.Unregister;
using EventHorizon.Game.Client.Engine.Systems.Module.Model;
using EventHorizon.Game.Client.Systems.Local.Modules.MeshManagement.Set;
using EventHorizon.Game.Client.Systems.Player.Api;
using EventHorizon.Game.Client.Systems.Player.Modules.Camera.Api;
using MediatR;

public class BabylonJSCameraModule : ModuleEntityBase, CameraModule, MeshSetEventObserver
{
    private readonly IMediator _mediator = GameServiceProvider.GetService<IMediator>();
    private readonly IPlayerEntity _entity;

    public override int Priority => 0;

    public BabylonJSCameraModule(IPlayerEntity playerEntity)
    {
        _entity = playerEntity;
    }

    public override async Task Initialize()
    {
        GamePlatform.RegisterObserver(this);
        await _mediator.Send(
            new ManageCameraCommand(
                SystemCameraTypes.PLAYER_UNIVERSAL_CAMERA_NAME,
                new BabylonJSUniversalCamera(
                    SystemCameraTypes.PLAYER_UNIVERSAL_CAMERA_NAME,
                    _entity
                )
            )
        );

        await _mediator.Send(
            new ManageCameraCommand(
                SystemCameraTypes.PLAYER_FOLLOW_CAMERA_NAME,
                new BabylonJSMeshRotationFollowCamera(
                    SystemCameraTypes.PLAYER_FOLLOW_CAMERA_NAME,
                    _entity
                )
            )
        );

        await _mediator.Send(
            new SetActiveCameraCommand(SystemCameraTypes.PLAYER_FOLLOW_CAMERA_NAME)
        );
    }

    public override async Task Dispose()
    {
        GamePlatform.UnRegisterObserver(this);
        await _mediator.Send(
            new DisposeOfCameraCommand(SystemCameraTypes.PLAYER_UNIVERSAL_CAMERA_NAME)
        );
        await _mediator.Send(
            new DisposeOfCameraCommand(SystemCameraTypes.PLAYER_FOLLOW_CAMERA_NAME)
        );
    }

    public override Task Update()
    {
        return Task.CompletedTask;
    }

    public async Task Handle(MeshSetEvent args)
    {
        if (args.ClientId != _entity.ClientId)
        {
            return;
        }

        await _mediator.Send(
            new DisposeOfCameraCommand(SystemCameraTypes.PLAYER_FOLLOW_CAMERA_NAME)
        );

        await _mediator.Send(
            new ManageCameraCommand(
                SystemCameraTypes.PLAYER_FOLLOW_CAMERA_NAME,
                new BabylonJSMeshRotationFollowCamera(
                    SystemCameraTypes.PLAYER_FOLLOW_CAMERA_NAME,
                    _entity
                )
            )
        );
        await _mediator.Send(
            new SetActiveCameraCommand(SystemCameraTypes.PLAYER_FOLLOW_CAMERA_NAME)
        );
    }
}
