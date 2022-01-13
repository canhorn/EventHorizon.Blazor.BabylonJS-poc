namespace EventHorizon.Game.Client.Systems.Local.Modules.MeshManagement.Model;

using System.Threading.Tasks;

using EventHorizon.Game.Client.Core.Factory.Api;
using EventHorizon.Game.Client.Core.Timer.Api;
using EventHorizon.Game.Client.Engine.Rendering.Api;
using EventHorizon.Game.Client.Engine.Systems.Entity.Api;
using EventHorizon.Game.Client.Engine.Systems.Mesh.Api;
using EventHorizon.Game.Client.Engine.Systems.Mesh.Model;
using EventHorizon.Game.Client.Engine.Systems.Module.Model;
using EventHorizon.Game.Client.Systems.ClientAssets.Reload;
using EventHorizon.Game.Client.Systems.Entity.Modules.ModelLoader.Loaded;
using EventHorizon.Game.Client.Systems.Entity.Properties.Model.Api;
using EventHorizon.Game.Client.Systems.Local.InView.Entering;
using EventHorizon.Game.Client.Systems.Local.InView.Exiting;
using EventHorizon.Game.Client.Systems.Local.Modules.MeshManagement.Api;
using EventHorizon.Game.Client.Systems.Local.Modules.MeshManagement.Set;
using EventHorizon.Game.Client.Systems.Map.Ready;

using MediatR;

public class MeshModuleOptions
{
    public bool SetRotation { get; }
    public bool SetScaling { get; }

    public MeshModuleOptions(
        bool setRotation,
        bool setScale
    )
    {
        SetRotation = setRotation;
        SetScaling = setScale;
    }
}

public class MeshModule
    : ModuleEntityBase,
      IMeshModule,
      MapMeshReadyEventObserver,
      MeshLoadedEventObserver,
      EntityEnteringViewEventObserver,
      EntityExitingViewEventObserver,
      ReloadingClientAssetsEventObserver
{
    private readonly IMediator _mediator;
    private readonly IObjectEntity _entity;
    private readonly MeshModuleOptions _options;

    private IIntervalTimerService? _timer;

    public override int Priority => 0;

    public IEngineMesh Mesh { get; private set; }

    public MeshModule(
        IObjectEntity entity,
        MeshModuleOptions options
    )
    {
        _mediator =
            GameServiceProvider.GetService<IMediator>();

        _entity = entity;
        _options = options;

        Mesh = BuildMesh();
    }

    public override Task Initialize()
    {
        GamePlatfrom.RegisterObserver(this);

        PositionMesh();

        _timer = GameServiceProvider
            .GetService<IFactory<IIntervalTimerService>>()
            .Create()
            .Setup(
                millisecondInterval: 100,
                onElapsed: () =>
                {
                    _entity.Transform.Position.Set(
                        Mesh.Position
                    );
                    return Task.CompletedTask;
                }
            )
            .Start();

        return Task.CompletedTask;
    }

    public override Task Dispose()
    {
        GamePlatfrom.UnRegisterObserver(this);
        if (_timer != null)
        {
            _timer.Dispose();
        }
        if (Mesh != null)
        {
            Mesh.Dispose();
        }
        return Task.CompletedTask;
    }

    public override Task Update()
    {
        return Task.CompletedTask;
    }

    private void PositionMesh()
    {
        Mesh.Position.Set(_entity.Transform.Position);

        if (
            _options.SetRotation
            && _entity.Transform.Rotation != null
        )
        {
            Mesh.Rotation.Set(_entity.Transform.Rotation);
        }

        if (
            _options.SetScaling
            && _entity.Transform.Scaling != null
        )
        {
            Mesh.Scaling.Set(_entity.Transform.Scaling);
        }

        if (_entity.Transform.ScalingDeterminant.HasValue)
        {
            Mesh.ScalingDeterminant =
                _entity.Transform.ScalingDeterminant.Value;
        }
        else
        {
            var modelState =
                _entity.GetProperty<IModelState>(
                    IModelState.NAME
                );
            if (
                modelState.IsNotNull()
                && modelState.ScalingDeterminant.HasValue
            )
            {
                Mesh.ScalingDeterminant =
                    modelState.ScalingDeterminant.Value;
            }
        }
    }

    private IEngineMesh BuildMesh()
    {
        var scene =
            GameServiceProvider
                .GetService<IRenderingScene>()
                .GetBabylonJSScene().Scene;
        return new BabylonJSEngineMesh(
            BabylonJS.Mesh.CreateSphere(
                $"testing_sphere_{_entity.ClientId}",
                segments: 8,
                diameter: 1,
                scene
            )
        );
    }

    public Task Handle(MapMeshReadyEvent args)
    {
        PositionMesh();

        return Task.CompletedTask;
    }

    public async Task Handle(MeshLoadedEvent args)
    {
        if (_entity.ClientId != args.ClientId)
        {
            return;
        }
        var meshToDispose = Mesh;
        Mesh = args.Mesh;
        PositionMesh();
        Mesh.SetEnabled(true);

        await _mediator.Publish(
            new MeshSetEvent(_entity.ClientId)
        );
        meshToDispose?.Dispose();
    }

    public Task Handle(EntityEnteringViewEvent args)
    {
        if (_entity.ClientId != args.ClientId)
        {
            return Task.CompletedTask;
        }
        Mesh.SetEnabled(true);
        return Task.CompletedTask;
    }

    public Task Handle(EntityExitingViewEvent args)
    {
        if (_entity.ClientId != args.ClientId)
        {
            return Task.CompletedTask;
        }
        Mesh.SetEnabled(false);
        return Task.CompletedTask;
    }

    public async Task Handle(
        ReloadingClientAssetsEvent args
    )
    {
        Mesh.Dispose();
        Mesh = BuildMesh();
        PositionMesh();
        await _mediator.Publish(
            new MeshSetEvent(_entity.ClientId)
        );
    }
}
