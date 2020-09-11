namespace EventHorizon.Game.Client.Systems.Entity.Modules.InteractionIndicator.Model
{
    using System;
    using System.Threading.Tasks;
    using EventHorizon.Game.Client.Engine.Core.Api;
    using EventHorizon.Game.Client.Engine.Systems.Entity.Api;
    using EventHorizon.Game.Client.Engine.Systems.Module.Model;
    using EventHorizon.Game.Client.Systems.Entity.Modules.InteractionIndicator.Api;
    using EventHorizon.Game.Client.Systems.Entity.Modules.InteractionIndicator.Clear;
    using EventHorizon.Game.Client.Systems.Entity.Modules.InteractionIndicator.Run;
    using EventHorizon.Game.Client.Systems.Entity.Modules.InteractionIndicator.Show;
    using EventHorizon.Game.Client.Systems.Particle.Api;
    using EventHorizon.Game.Client.Systems.Particle.Model;
    using EventHorizon.Game.Client.Systems.Player.Action.Model;
    using EventHorizon.Game.Client.Systems.Player.Action.Model.Send;
    using MediatR;

    public class StandardInteractionIndicatorModule
        : ModuleEntityBase,
        InteractionIndicatorModule,
        ShowInteractionIndicatorEventObserver,
        ClearInteractionIndicatorEventObserver,
        RunInteractionEventObserver
    {
        private static string INTERACTION_INDICATOR_TEMPLATE_ID => "Particle_Flame";

        private readonly IMediator _mediator = GameServiceProvider.GetService<IMediator>();

        private readonly IObjectEntity _entity;
        private readonly IParticleEmitter _particle;

        public override int Priority => 0;

        public StandardInteractionIndicatorModule(
            IObjectEntity entity
        )
        {
            _entity = entity;

            _particle = new StandardServerParticle(
                _entity,
                new ParticleEmitterOptions(
                    GameServiceProvider.GetService<IIndexPool>().NextIndex(),
                    INTERACTION_INDICATOR_TEMPLATE_ID,
                    true,
                    false
                )
            );
        }

        public override async Task Initialize()
        {
            var interactionState = _entity.GetPropertyAsOption<InteractionState>(
                InteractionState.NAME
            );
            if (!interactionState.HasValue
                || !interactionState.Value.Active)
            {
                // We are not active, so don't register as observer
                GamePlatfrom.RegisterObserver(
                    this
                );
                await _particle.Initialize();
                await _particle.PostInitialize();
            }
        }

        public override async Task Dispose()
        {
            GamePlatfrom.UnRegisterObserver(
                this
            );
            await _particle.Dispose();
        }

        public override Task Update()
        {
            return _particle.Update();
        }

        public async Task Handle(
            RunInteractionEvent args
        )
        {
            if (_particle.IsActive)
            {
                await _mediator.Publish(
                    new InvokePlayerActionEvent(
                        PlayerActions.INTERACT,
                        new PlayerInteractActionData(
                            _entity.EntityId
                        )
                    )
                );
            }
        }

        public async Task Handle(
            ShowInteractionIndicatorEvent args
        )
        {
            if (_particle.IsActive)
            {
                await _particle.Stop();
            }
            if (_entity.EntityId != args.EntityId)
            {
                return;
            }
            await _particle.Start();
        }

        public async Task Handle(
            ClearInteractionIndicatorEvent args
        )
        {
            if (_particle.IsActive)
            {
                await _particle.Stop();
            }
        }
    }
}
