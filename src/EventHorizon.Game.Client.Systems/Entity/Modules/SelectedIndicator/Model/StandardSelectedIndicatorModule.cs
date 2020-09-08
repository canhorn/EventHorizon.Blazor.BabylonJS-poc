namespace EventHorizon.Game.Client.Systems.Entity.Modules.SelectedIndicator.Model
{
    using System;
    using System.Threading.Tasks;
    using EventHorizon.Game.Client.Engine.Core.Api;
    using EventHorizon.Game.Client.Engine.Lifecycle.Register.Register;
    using EventHorizon.Game.Client.Engine.Systems.Entity.Api;
    using EventHorizon.Game.Client.Engine.Systems.Module.Model;
    using EventHorizon.Game.Client.Systems.Entity.Modules.SelectedIndicator.Api;
    using EventHorizon.Game.Client.Systems.Local.ScreenPointer.Entity;
    using EventHorizon.Game.Client.Systems.Particle.Api;
    using EventHorizon.Game.Client.Systems.Particle.Model;
    using EventHorizon.Game.Server.Actions.Agent;
    using Microsoft.Extensions.Logging;

    public class StandardSelectedIndicatorModule
        : ModuleEntityBase,
        SelectedIndicatorModule,
        PointerHitEntityEventObserver,
        ClearPointerHitEntityEventObserver
    {
        private static string SELECTED_INDICATOR_TEMPLATE_ID => "Particle_SelectedIndicator";

        private readonly ILogger _logger = GameServiceProvider.GetService<ILogger<StandardSelectedIndicatorModule>>();

        private readonly IObjectEntity _entity;
        private readonly IParticleEmitter _activeParticle;

        public override int Priority => 0;

        public StandardSelectedIndicatorModule(
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
            PointerHitEntityEvent args
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
