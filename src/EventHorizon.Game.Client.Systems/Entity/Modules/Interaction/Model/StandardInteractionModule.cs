namespace EventHorizon.Game.Client.Systems.Entity.Modules.Interaction.Model
{
    using System.Linq;
    using System.Threading.Tasks;

    using EventHorizon.Game.Client.Core.Factory.Api;
    using EventHorizon.Game.Client.Core.Timer.Api;
    using EventHorizon.Game.Client.Engine.Systems.Entity.Api;
    using EventHorizon.Game.Client.Engine.Systems.Module.Model;
    using EventHorizon.Game.Client.Systems.Entity.Modules.Interaction.Api;
    using EventHorizon.Game.Client.Systems.Entity.Properties.Interaction.Api;
    using EventHorizon.Game.Client.Systems.Player.Modules.PlayerInteraction.WithIn;
    using EventHorizon.Game.Client.Systems.Player.Query;

    using MediatR;

    public class StandardInteractionModule
        : ModuleEntityBase,
        InteractionModule
    {
        private readonly IMediator _mediator = GameServiceProvider.GetService<IMediator>();
        private readonly IIntervalTimerService _playerCheckInterval = GameServiceProvider.GetService<IFactory<IIntervalTimerService>>().Create();

        private readonly IObjectEntity _entity;

        private bool _inDistance;
        private InteractionState? _interactionState;
        private int _interactionDistanceToPlayer = 10;

        public override int Priority => 0;

        public StandardInteractionModule(
            IObjectEntity entity
        )
        {
            _entity = entity;
        }

        public override Task Initialize()
        {
            var interactionStateOption = _entity.GetPropertyAsOption<InteractionState>(
                InteractionState.NAME
            );
            if (!interactionStateOption.HasValue
                || !interactionStateOption.Value.Active
            )
            {
                return Task.CompletedTask;
            }
            _interactionState = interactionStateOption.Value;

            var entityConfiguration = _entity.GetEntityConfiguration();
            var interactionConfig = entityConfiguration.Get(
                InteractionConfiguration.KEY,
                () => new InteractionConfiguration()
            );

            _playerCheckInterval.Setup(
                interactionConfig.CheckInterval,
                CheckForPlayerInDistance
            );
            _playerCheckInterval.Start();

            _interactionDistanceToPlayer = _interactionState.List?
                .Select(a => a.DistanceToPlayer)
                .Max() ?? _interactionState.DistanceToPlayer;


            return Task.CompletedTask;
        }

        public override Task Dispose()
        {
            _playerCheckInterval.Dispose();
            return Task.CompletedTask;
        }

        public override Task Update()
        {
            return Task.CompletedTask;
        }

        private async Task CheckForPlayerInDistance()
        {
            var playerResult = await _mediator.Send(
                new QueryForCurrentPlayer()
            );
            if (!playerResult.Success
                || _entity.EntityId == playerResult.Result.EntityId
                || _interactionState.IsNull()
            )
            {
                return;
            }

            var player = playerResult.Result;
            var playerPosition = player.Transform.Position;
            var currentPosition = _entity.Transform.Position;
            var toPlayer = playerPosition.Subtract(currentPosition);
            var distanceToPlayer = toPlayer.Length();

            if (distanceToPlayer <= _interactionDistanceToPlayer)
            {
                await _mediator.Publish(
                    new EntityWithinInteractionDistanceEvent(
                        _entity,
                        distanceToPlayer
                    )
                );
                _inDistance = true;
                return;
            }

            if (_inDistance)
            {
                await _mediator.Publish(
                    new EntityLeftInteractionDistanceEvent(
                        _entity
                    )
                );
                _inDistance = false;
            }
        }
    }
}
