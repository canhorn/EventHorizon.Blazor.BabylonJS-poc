namespace EventHorizon.Game.Client.Systems.Player.Modules.PlayerInteraction.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using EventHorizon.Game.Client.Engine.Systems.ClientAction.Publish;
    using EventHorizon.Game.Client.Engine.Systems.Entity.Api;
    using EventHorizon.Game.Client.Engine.Systems.Module.Model;
    using EventHorizon.Game.Client.Systems.Entity.Modules.InteractionIndicator.Clear;
    using EventHorizon.Game.Client.Systems.Entity.Modules.InteractionIndicator.Run;
    using EventHorizon.Game.Client.Systems.Entity.Modules.InteractionIndicator.Show;
    using EventHorizon.Game.Client.Systems.Player.Action.Model;
    using EventHorizon.Game.Client.Systems.Player.Action.Model.Send;
    using EventHorizon.Game.Client.Systems.Player.Api;
    using EventHorizon.Game.Client.Systems.Player.Modules.PlayerInteraction.Api;
    using EventHorizon.Game.Client.Systems.Player.Modules.PlayerInteraction.ClientAction;
    using EventHorizon.Game.Client.Systems.Player.Modules.PlayerInteraction.WithIn;
    using MediatR;

    public class StandardPlayerInteractionModule
        : ModuleEntityBase,
        PlayerInteractionModule,
        ClientActionServerInteractionEventObserver,
        EntityWithinInteractionDistanceEventObserver,
        EntityLeftInteractionDistanceEventObserver,
        RunInteractionEventObserver
    {
        private readonly IMediator _mediator = GameServiceProvider.GetService<IMediator>();

        private readonly IDictionary<long, InteractionItem> _distanceEntityMap = new Dictionary<long, InteractionItem>();
        private readonly IPlayerEntity _entity;

        public override int Priority => 0;

        public StandardPlayerInteractionModule(
            IPlayerEntity playerEntity
        )
        {
            _entity = playerEntity;
        }

        public override Task Initialize()
        {
            GamePlatfrom.RegisterObserver(
                this
            );
            return Task.CompletedTask;
        }

        public override Task Dispose()
        {
            GamePlatfrom.UnRegisterObserver(
                this
            );
            return Task.CompletedTask;
        }

        public override Task Update()
        {
            return Task.CompletedTask;
        }

        public async Task Handle(
            ClientActionServerInteractionEvent args
        )
        {
            await _mediator.Send(
                new PublishClientActionCommand(
                    args.CommandType,
                    args.Data
                )
            );
        }

        public Task Handle(
            EntityWithinInteractionDistanceEvent args
        )
        {
            _distanceEntityMap[args.Entity.EntityId] = new InteractionItem(
                args.Entity,
                args.DistanceToPlayer
            );
            return CalculateFocus();
        }

        public Task Handle(
            EntityLeftInteractionDistanceEvent args
        )
        {
            _distanceEntityMap.Remove(
                args.Entity.EntityId
            );
            return CalculateFocus();
        }

        public async Task Handle(
            RunInteractionEvent args
        )
        {
            var interactionItemList = _distanceEntityMap
                .OrderBy(a => a.Value.DistanceToPlayer)
                .Select(a => a.Value);
            if (interactionItemList.Any())
            {
                await _mediator.Publish(
                    new InvokePlayerActionEvent(
                        PlayerActions.INTERACT,
                        new PlayerInteractActionData(
                            interactionItemList.First().Entity.EntityId
                        )
                    )
                );
            }
        }

        private async Task CalculateFocus()
        {
            var interactionItemList = _distanceEntityMap
                .OrderBy(a => a.Value.DistanceToPlayer)
                .Select(a => a.Value);
            if (interactionItemList.Any())
            {
                await _mediator.Publish(
                    new ShowInteractionIndicatorEvent(
                        interactionItemList
                            .First().Entity.EntityId
                    )
                );
                return;
            }

            await _mediator.Publish(
                new ClearInteractionIndicatorEvent()
            );
        }

        private struct InteractionItem
        {
            public IObjectEntity Entity { get; }
            public decimal DistanceToPlayer { get; }

            public InteractionItem(
                IObjectEntity entity,
                decimal distanceToPlayer
            )
            {
                Entity = entity;
                DistanceToPlayer = distanceToPlayer;
            }
        }
    }
}
