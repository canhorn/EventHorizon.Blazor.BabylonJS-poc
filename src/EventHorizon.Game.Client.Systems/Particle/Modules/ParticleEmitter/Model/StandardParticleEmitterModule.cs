namespace EventHorizon.Game.Client.Systems.Particle.Modules.ParticleEmitter.Model
{
    using System;
    using System.Threading.Tasks;
    using EventHorizon.Game.Client.Engine.Particle.Create;
    using EventHorizon.Game.Client.Engine.Particle.Dispose;
    using EventHorizon.Game.Client.Engine.Particle.Model;
    using EventHorizon.Game.Client.Engine.Particle.Start;
    using EventHorizon.Game.Client.Engine.Systems.Entity.Api;
    using EventHorizon.Game.Client.Engine.Systems.Module.Model;
    using EventHorizon.Game.Client.Systems.Local.Modules.MeshManagement.Api;
    using EventHorizon.Game.Client.Systems.Local.Modules.MeshManagement.Set;
    using EventHorizon.Game.Client.Systems.Particle.Model;
    using EventHorizon.Game.Client.Systems.Particle.Modules.ParticleEmitter.Api;
    using MediatR;

    public class StandardParticleEmitterModule
        : ModuleEntityBase,
        ParticleEmitterModule,
        MeshSetEventObserver
    {
        private readonly IMediator _mediator = GameServiceProvider.GetService<IMediator>();
        private readonly IObjectEntity _entity;
        private readonly ParticleEmitterOptions _options;

        public StandardParticleEmitterModule(
            IObjectEntity entity,
            ParticleEmitterOptions options
        )
        {
            _entity = entity;
            _options = options;
        }

        public override int Priority => 0;

        public override async Task Initialize()
        {
            GamePlatfrom.RegisterObserver(this);
            await CreateParticle();
        }

        public override Task Dispose()
        {
            GamePlatfrom.UnRegisterObserver(this);
            return Task.CompletedTask;
        }

        public override Task Update()
        {
            return Task.CompletedTask;
        }

        public async Task Handle(
            MeshSetEvent args
        )
        {
            if (_entity.ClientId != args.ClientId)
            {
                return;
            }
            await CreateParticle();
        }

        private async Task CreateParticle()
        {
            var meshModule = _entity.GetModule<IMeshModule>(
                IMeshModule.MODULE_NAME
            );
            if (meshModule == null)
            {
                return;
            }

            // Dispose of any existing particles by _options.ParticleId
            await _mediator.Send(
                new DisposeOfParticleSystemCommand(
                    _options.ParticleId
                )
            );

            if (!_options.IgnoreMeshVisibility)
            {
                meshModule.Mesh.SetVisible(false);
            }
            await _mediator.Send(
                new CreateParticleFromTemplateCommand(
                    _options.ParticleId,
                    _options.TemplateId,
                    new ParticleSettingsModel
                    {
                    { "name", "Particle_Emitter" },
                    { "emitter", meshModule.Mesh },
                    }
                )
            );
            if (_options.StartAfterCreated)
            {
                await _mediator.Send(
                    new StartParticleSystemCommand(
                        _options.ParticleId
                    )
                );
            }
        }
    }
}
