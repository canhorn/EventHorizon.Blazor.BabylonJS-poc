namespace EventHorizon.Game.Client.Systems.Particle.Model;

using System;
using System.Threading.Tasks;

using EventHorizon.Game.Client.Engine.Entity.Api;
using EventHorizon.Game.Client.Engine.Lifecycle.Model;
using EventHorizon.Game.Client.Engine.Particle.Dispose;
using EventHorizon.Game.Client.Engine.Particle.Start;
using EventHorizon.Game.Client.Engine.Particle.Stop;
using EventHorizon.Game.Client.Engine.Systems.Entity.Api;
using EventHorizon.Game.Client.Engine.Systems.Entity.Model;
using EventHorizon.Game.Client.Systems.Particle.Api;
using EventHorizon.Game.Client.Systems.Particle.Modules.ParticleEmitter.Api;
using EventHorizon.Game.Client.Systems.Particle.Modules.ParticleEmitter.Model;

using MediatR;

public class StandardServerParticle : ClientLifecycleEntityBase, ParticleEmitter
{
    private readonly IMediator _mediator =
        GameServiceProvider.GetService<IMediator>();
    private readonly IObjectEntity _entity;
    private readonly ParticleEmitterOptions _options;

    public bool IsActive { get; private set; }

    public StandardServerParticle(
        IObjectEntity entity,
        ParticleEmitterOptions options
    )
        : base(
            new ObjectEntityDetailsModel
            {
                Id = options.ParticleId,
                Name =
                    $"server_particle-{options.TemplateId}-{entity.ClientId}-{options.ParticleId}",
                GlobalId =
                    $"server_particle-{options.TemplateId}-{entity.ClientId}-{options.ParticleId}",
                Type = "SERVER_PARTICLE",
            }
        )
    {
        _entity = entity;
        _options = options;
    }

    public override Task Initialize()
    {
        Setup();

        return Task.CompletedTask;
    }

    public override async Task Dispose()
    {
        await _mediator.Send(
            new DisposeOfParticleSystemCommand(_options.ParticleId)
        );
        await base.Dispose();
    }

    public override Task Draw()
    {
        return Task.CompletedTask;
    }

    public async Task Start()
    {
        await _mediator.Send(
            new StartParticleSystemCommand(_options.ParticleId)
        );
        IsActive = true;
    }

    public async Task Stop()
    {
        await _mediator.Send(
            new StopParticleSystemCommand(_options.ParticleId)
        );
        IsActive = false;
    }

    public void MoveTo(IVector3 position) { }

    private void Setup()
    {
        RegisterModule(
            ParticleEmitterModule.MODULE_NAME,
            new StandardParticleEmitterModule(_entity, _options)
        );
    }
}
