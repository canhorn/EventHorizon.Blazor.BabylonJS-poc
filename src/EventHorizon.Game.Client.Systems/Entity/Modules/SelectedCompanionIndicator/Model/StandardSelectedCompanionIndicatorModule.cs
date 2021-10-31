namespace EventHorizon.Game.Client.Systems.Entity.Modules.SelectedCompanionIndicator.Model
{
    using System.Threading.Tasks;

    using EventHorizon.Game.Client.Engine.Core.Api;
    using EventHorizon.Game.Client.Engine.Systems.Entity.Api;
    using EventHorizon.Game.Client.Engine.Systems.Module.Model;
    using EventHorizon.Game.Client.Systems.Entity.Modules.SelectedCompanionIndicator.Api;
    using EventHorizon.Game.Client.Systems.Entity.Modules.SelectedCompanionIndicator.Selected;
    using EventHorizon.Game.Client.Systems.Entity.Properties.Selection.Api;
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
        private readonly ILogger _logger = GameServiceProvider.GetService<ILogger<StandardSelectedCompanionIndicatorModule>>();

        private readonly IObjectEntity _entity;
        private readonly ParticleEmitter _activeParticle;

        public override int Priority => 0;

        public StandardSelectedCompanionIndicatorModule(
            IObjectEntity entity
        )
        {
            _entity = entity;
            var particleTemplateId = string.Empty;

            var selectedStateOption = _entity.GetPropertyAsOption<SelectionState>(
                SelectionState.NAME
            );
            if (selectedStateOption.HasValue)
            {
                particleTemplateId = selectedStateOption.Value.SelectedCompanionParticleTemplate;
                if (selectedStateOption.Value.SelectedCompanionParticleTemplate.IsNullOrEmpty())
                {
                    _logger.LogPropertyMissing(
                        nameof(IObjectEntity),
                        nameof(SelectionState),
                        nameof(SelectionState.SelectedCompanionParticleTemplate)
                    );
                }
            }

            _activeParticle = new StandardServerParticle(
                _entity,
                new ParticleEmitterOptions(
                    GameServiceProvider.GetService<IIndexPool>().NextIndex(),
                    particleTemplateId,
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
