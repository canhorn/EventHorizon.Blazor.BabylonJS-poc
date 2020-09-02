namespace EventHorizon.Game.Client.Systems.Entity.Modules.Details.Model
{
    using System;
    using System.Threading.Tasks;
    using EventHorizon.Game.Client.Engine.Systems.Entity.Api;
    using EventHorizon.Game.Client.Engine.Systems.Module.Model;
    using EventHorizon.Game.Client.Systems.Entity.Actions;
    using EventHorizon.Game.Client.Systems.Entity.Changed;
    using EventHorizon.Game.Client.Systems.Entity.Modules.Details.Api;
    using EventHorizon.Observer.Register;
    using EventHorizon.Observer.Unregister;
    using MediatR;

    public class DetailsModule
        : ModuleEntityBase,
        IDetailsModule,
        ClientActionEntityChangedEventObserver
    {
        private readonly IMediator _mediator;
        private readonly IObjectEntity _entity;

        public override int Priority => 0;

        public DetailsModule(
            IObjectEntity entity
        )
        {
            _mediator = GameServiceProvider.GetService<IMediator>();
            _entity = entity;
        }

        public override async Task Initialize()
        {
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

        public async Task Handle(
            ClientActionEntityClientChangedEvent args
        )
        {
            if (_entity.EntityId != args.Details.Id)
            {
                return;
            }
            _entity.UpdateDetails(
                args.Details
            );
            await _mediator.Publish(
                new EntityChangedSuccessfullyEvent(
                    _entity.EntityId
                )
            );
        }
    }
}
