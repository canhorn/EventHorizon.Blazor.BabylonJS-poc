namespace EventHorizon.Game.Client.Systems.Entity.Modules.Move.Model
{
    using System;
    using System.Threading.Tasks;
    using EventHorizon.Game.Client.Engine.Entity.Api;
    using EventHorizon.Game.Client.Engine.Entity.Model;
    using EventHorizon.Game.Client.Engine.Systems.Entity.Api;
    using EventHorizon.Game.Client.Engine.Systems.Module.Model;
    using EventHorizon.Game.Client.Systems.Entity.ClientAction;
    using EventHorizon.Game.Client.Systems.Entity.Modules.Move.Api;
    using EventHorizon.Game.Client.Systems.Entity.Moving;
    using EventHorizon.Game.Client.Systems.Entity.States.Move.Model;
    using EventHorizon.Game.Client.Systems.Height.Api;
    using EventHorizon.Game.Client.Systems.Local.Modules.State.Api;
    using EventHorizon.Observer.Register;
    using EventHorizon.Observer.Unregister;
    using MediatR;

    public class MoveModule
        : ModuleEntityBase,
        IMoveModule,
        ClientActionEntityMoveEventObserver
    {
        private readonly IMediator _mediator;
        private readonly IHeightResolver _heightResolver;
        private readonly IObjectEntity _entity;

        private IStateModule? _stateModule;

        private IVector3? _currentMoveTo;

        public override int Priority => 1000;

        public MoveModule(
            IObjectEntity entity
        ) : base()
        {
            _mediator = GameServiceProvider.GetService<IMediator>();
            _heightResolver = GameServiceProvider.GetService<IHeightResolver>();
            _entity = entity;
        }

        public override async Task Initialize()
        {
            _stateModule = _entity.GetModule<IStateModule>(
                IStateModule.MODULE_NAME
            );
            await _mediator.Send(
                new RegisterObserverCommand(
                    this
                )
            );
        }

        public override async Task Dispose()
        {
            await _mediator.Send(
                new UnregisterObserverCommand(
                    this
                )
            );
        }

        public override Task Update()
        {
            return Task.CompletedTask;
        }

        public void SetCurrentMoveTo(
            IVector3 position
        )
        {
            _currentMoveTo = position;
        }

        public async Task Handle(
            ClientActionEntityMoveEvent args
        )
        {
            if (args.EntityId != _entity.EntityId 
                || args.MoveTo == null)
            {
                return;
            }
            var moveTo = MapMoveTo(
                args.MoveTo
            );
            if (_currentMoveTo == moveTo)
            {
                return;
            }
            _currentMoveTo = moveTo;
            _stateModule!.Clear();
            _stateModule!.Add(
                new MoveState(
                    _entity,
                    "move_entity",
                    0.3m,
                    new IVector3[] { _currentMoveTo }
                )
            );
            await _mediator.Publish(
                new EntityMovingEvent(
                    _entity.ClientId
                )
            );
        }

        private IVector3 MapMoveTo(
            IVector3 moveTo
        )
        {
            return new StandardVector3(
                moveTo.X,
                _heightResolver.FindHeight(
                    moveTo.X,
                    moveTo.Z
                ),
                moveTo.Z
            );
        }
    }
}
