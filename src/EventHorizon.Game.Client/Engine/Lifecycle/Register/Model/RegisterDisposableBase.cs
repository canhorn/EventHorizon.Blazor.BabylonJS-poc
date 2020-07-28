namespace EventHorizon.Game.Client.Engine.Lifecycle.Register.Model
{
    using System.Threading.Tasks;
    using EventHorizon.Game.Client.Engine.Lifecycle.Api;
    using EventHorizon.Game.Client.Engine.Lifecycle.Register.Api;
    using EventHorizon.Game.Client.Engine.Lifecycle.Register.Disposed;
    using MediatR;

    public class RegisterDisposableBase
        : RegisterBase<IDisposableEntity>, IRegisterDisposable
    {
        private readonly IMediator _mediator;

        public RegisterDisposableBase(
            IMediator mediator
        )
        {
            _mediator = mediator;
        }
        public override async Task Run()
        {
            foreach (var entity in _entityList)
            {
                await entity.Dispose();
                await _mediator.Publish(
                    new EntityDisposedEvent(
                        entity
                    )
                );
            }
            _entityList.Clear();
        }
    }
}
