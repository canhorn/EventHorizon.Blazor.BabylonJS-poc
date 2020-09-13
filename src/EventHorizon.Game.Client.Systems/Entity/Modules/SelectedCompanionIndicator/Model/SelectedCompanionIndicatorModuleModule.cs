namespace EventHorizon.Game.Client.Systems.Entity.Modules.SelectedCompanionIndicator.Model
{
    using System;
    using System.Threading.Tasks;
    using EventHorizon.Game.Client.Engine.Core.Api;
    using EventHorizon.Game.Client.Engine.Systems.Entity.Api;
    using EventHorizon.Game.Client.Engine.Systems.Module.Model;
    using EventHorizon.Game.Client.Systems.Entity.Modules.SelectedCompanionIndicator.Api;
    using EventHorizon.Game.Client.Systems.Entity.Modules.SelectedCompanionIndicator.Selected;
    using EventHorizon.Game.Client.Systems.Particle.Api;
    using EventHorizon.Game.Client.Systems.Particle.Model;
    using EventHorizon.Game.Server.ClientAction.Agent;
    using Microsoft.Extensions.Logging;

    public class StandardSelectedCompanionIndicatorModule
        : ModuleEntityBase,
        SelectedCompanionIndicatorModule,
        CompanionSelectedEventObserver,
        ClearPointerHitEntityEventObserver
    {
        private static string SELECTED_INDICATOR_TEMPLATE_ID => "Particle_SelectedCompanionIndicator";

        private readonly ILogger _logger = GameServiceProvider.GetService<ILogger<StandardSelectedCompanionIndicatorModule>>();

        private readonly IObjectEntity _entity;
        private readonly ParticleEmitter _activeParticle;

        public override int Priority => 0;

        public StandardSelectedCompanionIndicatorModule(
            IObjectEntity entity
        )
        {
            _entity = entity;

            _activeParticle = new StandardServerParticle(
                _entity,
                new ParticleEmitterOptions(
                    GameServiceProvider.GetService<IIndexPool>().NextIndex(),
                    SELECTED_INDICATOR_TEMPLATE_ID,
                    true,
                    false
                )
            );
        }

        public override async Task Initialize()
        {
            GamePlatfrom.RegisterObserver(
                this
            );

            await _activeParticle.Initialize();
            await _activeParticle.PostInitialize();
        }

        public override async Task Dispose()
        {
            GamePlatfrom.UnRegisterObserver(
                this
            );
            await _activeParticle.Dispose();
        }

        public override Task Update()
        {
            return _activeParticle.Update();
        }

        public async Task Handle(
            CompanionSelectedEvent args
        )
        {
            await _activeParticle.Stop();
            if (_entity.EntityId != args.EntityId)
            {
                return;
            }
            await _activeParticle.Start();
        }

        public async Task Handle(
            ClearPointerHitEntityEvent args
        )
        {
            await _activeParticle.Stop();
        }
    }
}
