namespace EventHorizon.Game.Client.Systems.Particle.Model;

using System;
using System.Threading.Tasks;

using EventHorizon.Game.Client.Engine.Core.Api;
using EventHorizon.Game.Client.Engine.Entity.Api;
using EventHorizon.Game.Client.Engine.Lifecycle.Model;
using EventHorizon.Game.Client.Engine.Particle.Dispose;
using EventHorizon.Game.Client.Engine.Particle.Start;
using EventHorizon.Game.Client.Engine.Particle.Stop;
using EventHorizon.Game.Client.Engine.Systems.Entity.Model;
using EventHorizon.Game.Client.Systems.Entity.Modules.ModelLoader.Api;
using EventHorizon.Game.Client.Systems.Entity.Modules.ModelLoader.Model;
using EventHorizon.Game.Client.Systems.Entity.Modules.Move.Api;
using EventHorizon.Game.Client.Systems.Entity.Modules.Move.Model;
using EventHorizon.Game.Client.Systems.Entity.Properties.Model.Api;
using EventHorizon.Game.Client.Systems.Entity.Properties.Model.Model;
using EventHorizon.Game.Client.Systems.Entity.Properties.Move.Api;
using EventHorizon.Game.Client.Systems.Entity.Properties.Move.Model;
using EventHorizon.Game.Client.Systems.Local.Modules.MeshManagement.Api;
using EventHorizon.Game.Client.Systems.Local.Modules.MeshManagement.Model;
using EventHorizon.Game.Client.Systems.Local.Modules.State.Api;
using EventHorizon.Game.Client.Systems.Local.Modules.State.Model;
using EventHorizon.Game.Client.Systems.Local.Modules.Transform.Api;
using EventHorizon.Game.Client.Systems.Local.Modules.Transform.Model;
using EventHorizon.Game.Client.Systems.Particle.Api;
using EventHorizon.Game.Client.Systems.Particle.Modules.ParticleEmitter.Api;
using EventHorizon.Game.Client.Systems.Particle.Modules.ParticleEmitter.Model;

using MediatR;

public class StandardParticleEmitter
    : ClientLifecycleEntityBase,
        ParticleEmitter
{
    private readonly IMediator _mediator =
        GameServiceProvider.GetService<IMediator>();
    private readonly string _templateId;
    private readonly decimal _speed;

    private long _particleId = -1;
    private IVector3? _currentMoveTo;

    public bool IsActive { get; } = true;

    public StandardParticleEmitter(
        string templateId,
        IVector3 position,
        decimal speed
    )
        : base(
            new ObjectEntityDetailsModel
            {
                Name = $"particle-{templateId}",
                GlobalId = $"particle-{templateId}",
                Type = "CLIENT_PARTICLE_EMITTER",
                Transform = new ServerTransform
                {
                    Position = new ServerVector3(position)
                }
            }
        )
    {
        _templateId = templateId;
        _speed = speed;
    }

    public override Task Initialize()
    {
        Setup();

        return Task.CompletedTask;
    }

    public override async Task Dispose()
    {
        await _mediator.Send(new DisposeOfParticleSystemCommand(_particleId));
        await base.Dispose();
    }

    public override Task Draw()
    {
        return Task.CompletedTask;
    }

    public async Task Start()
    {
        await _mediator.Send(new StartParticleSystemCommand(_particleId));
    }

    public async Task Stop()
    {
        await _mediator.Send(new StopParticleSystemCommand(_particleId));
    }

    public void MoveTo(IVector3 position)
    {
        var module = GetModule<IMoveModule>(IMoveModule.MODULE_NAME);
        _currentMoveTo = position;
        if (module.IsNotNull())
        {
            module.SetCurrentMoveTo(position);
        }
    }

    private void Setup()
    {
        _particleId = GameServiceProvider.GetService<IIndexPool>().NextIndex();
        SetProperty(
            IModelState.NAME,
            new ModelStateModel
            {
                Mesh = new StandardModelMesh { AssetId = "BOX", },
            }
        );
        SetProperty(
            IMovementState.NAME,
            new MovementStateModel { Speed = _speed, }
        );
        RegisterModule(ITransformModule.MODULE_NAME, new TransformModule(this));
        RegisterModule(
            IModelLoaderModule.MODULE_NAME,
            new ModelLoaderModule(this)
        );
        RegisterModule(IStateModule.MODULE_NAME, new StateModule(this));
        RegisterModule(
            IMeshModule.MODULE_NAME,
            new MeshModule(this, new MeshModuleOptions(false, false))
        );
        var moveModule = new MoveModule(this);
        if (_currentMoveTo.IsNotNull())
        {
            moveModule.SetCurrentMoveTo(_currentMoveTo);
        }
        RegisterModule(IMoveModule.MODULE_NAME, moveModule);
        RegisterModule(
            ParticleEmitterModule.MODULE_NAME,
            new StandardParticleEmitterModule(
                this,
                new ParticleEmitterOptions(_particleId, _templateId)
            )
        );
    }
}
