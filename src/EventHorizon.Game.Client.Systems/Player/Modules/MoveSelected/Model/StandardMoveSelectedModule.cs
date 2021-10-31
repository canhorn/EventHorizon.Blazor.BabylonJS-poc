namespace EventHorizon.Game.Client.Systems.Player.Modules.MoveSelected.Model
{
    using System.Threading.Tasks;

    using EventHorizon.Game.Client.Engine.Entity.Api;
    using EventHorizon.Game.Client.Engine.Systems.Module.Model;
    using EventHorizon.Game.Client.Systems.Map.Hit;
    using EventHorizon.Game.Client.Systems.Player.Action.Api;
    using EventHorizon.Game.Client.Systems.Player.Action.Model.Send;
    using EventHorizon.Game.Client.Systems.Player.Api;
    using EventHorizon.Game.Client.Systems.Player.Modules.MoveSelected.Api;
    using EventHorizon.Game.Client.Systems.Player.Modules.SelectedCompanionTracker.Api;

    using MediatR;

    public class StandardMoveSelectedModule
        : ModuleEntityBase,
        MoveSelectedModule,
        MapMeshHitEventObserver
    {
        private readonly IMediator _mediator = GameServiceProvider.GetService<IMediator>();
        private readonly IPlayerEntity _entity;

        public override int Priority => 0;

        public StandardMoveSelectedModule(
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
            MapMeshHitEvent args
        )
        {
            var selectedCompanionTrackerModule = _entity.GetModule<SelectedCompanionTrackerModule>(
                SelectedCompanionTrackerModule.MODULE_NAME
            );

            if (selectedCompanionTrackerModule.IsNotNull()
                && selectedCompanionTrackerModule.HasSelectedEntity
            )
            {
                // TODO: AB#372 - [Combat] - Move into Player Property
                // The skillId from ObjectEntityConfiguration["moveSelectedConfig"]
                await _mediator.Publish(
                    new InvokePlayerActionEvent(
                        "Player.RUN_SKILL",
                        new PlayerRunSkillActionData(
                            _entity.EntityId,
                            selectedCompanionTrackerModule.SelectedEntityId,
                            args.Poisition,
                            "Skills_MoveTo.json"
                        )
                    )
                );
            }
        }

        public class PlayerRunSkillActionData
            : IPlayerActionData
        {
            public long CasterId { get; }
            public long TargetId { get; }
            public IVector3 TargetPosition { get; }
            public string SkillId { get; }

            public PlayerRunSkillActionData(
                long casterId,
                long targetId,
                IVector3 targetPosition,
                string skillId
            )
            {
                CasterId = casterId;
                TargetId = targetId;
                TargetPosition = targetPosition;
                SkillId = skillId;
            }
        }
    }
}
