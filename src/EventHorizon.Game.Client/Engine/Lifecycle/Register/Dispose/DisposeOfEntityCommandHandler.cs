namespace EventHorizon.Game.Client.Engine.Lifecycle.Register.Dispose
{
    using System.Threading;
    using System.Threading.Tasks;
    using EventHorizon.Game.Client.Engine.Lifecycle.Api;
    using EventHorizon.Game.Client.Engine.Lifecycle.Register.Disposed;
    using EventHorizon.Game.Client.Engine.Lifecycle.Register.Unregister;
    using MediatR;
    using MediatR.Pipeline;

    public class DisposeOfEntityCommandHandler
        : IRequestHandler<DisposeOfEntityCommand>
    {
        private readonly IMediator _mediator;

        public DisposeOfEntityCommandHandler(
            IMediator mediator
        )
        {
            _mediator = mediator;
        }

        public async Task<Unit> Handle(
            DisposeOfEntityCommand request,
            CancellationToken cancellationToken
        )
        {
            if (request.Entity is ILifecycleEntity lifecycleEntity)
            {
                await _mediator.Publish(
                    new UnregisterEntityEvent(
                        lifecycleEntity
                    )
                );
            }

            await request.Entity.Dispose();

            await _mediator.Publish(
                new EntityDisposedEvent(
                    request.Entity
                )
            );

            return Unit.Value;
        }
    }
}
