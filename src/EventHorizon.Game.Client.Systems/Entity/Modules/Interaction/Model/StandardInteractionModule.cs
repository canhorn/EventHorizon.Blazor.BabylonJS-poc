namespace EventHorizon.Game.Client.Systems.Entity.Modules.Interaction.Model
{
    using System;
    using System.Threading.Tasks;
    using EventHorizon.Game.Client.Core.Factory.Api;
    using EventHorizon.Game.Client.Core.Timer.Api;
    using EventHorizon.Game.Client.Engine.Systems.Entity.Api;
    using EventHorizon.Game.Client.Engine.Systems.Module.Model;
    using EventHorizon.Game.Client.Systems.Entity.Modules.Interaction.Api;
    using EventHorizon.Game.Client.Systems.Player.Modules.PlayerInteraction.WithIn;
    using EventHorizon.Game.Client.Systems.Player.Query;
    using MediatR;
    using Microsoft.Extensions.Logging;

    public class StandardInteractionModule
        : ModuleEntityBase,
        InteractionModule
    {
        private readonly ILogger _logger = GameServiceProvider.GetService<ILogger<StandardInteractionModule>>();
        private readonly IMediator _mediator = GameServiceProvider.GetService<IMediator>();
        private readonly IIntervalTimerService _playerCheckInterval = GameServiceProvider.GetService<IFactory<IIntervalTimerService>>().Create();

        private readonly IObjectEntity _entity;

        private bool _inDistance;

        public override int Priority => 0;

        public StandardInteractionModule(
            IObjectEntity entity
        )
        {
            _entity = entity;
        }

        public override Task Initialize()
        {
            _playerCheckInterval.Setup(
                100, // TODO: [PlatformSetting] - This is a configuration point
                CheckForPlayerInDistance
            );
            _playerCheckInterval.Start();

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
            )
            {
                return;
            }

            var player = playerResult.Result;
            var playerPosition = player.Transform.Position;
            var currentPosition = _entity.Transform.Position;
            var toPlayer = playerPosition.Subtract(currentPosition);
            var distanceToPlayer = toPlayer.Length();

            // TODO: Move to Entity Property State, so it can be customized
            if (distanceToPlayer <= 10)
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
