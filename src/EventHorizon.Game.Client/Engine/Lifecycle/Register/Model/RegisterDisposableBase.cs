namespace EventHorizon.Game.Client.Engine.Lifecycle.Register.Model
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using EventHorizon.Game.Client.Engine.Lifecycle.Api;
    using EventHorizon.Game.Client.Engine.Lifecycle.Register.Api;
    using EventHorizon.Game.Client.Engine.Lifecycle.Register.Disposed;
    using MediatR;
    using Microsoft.Extensions.Logging;

    public class RegisterDisposableBase
        : RegisterBase<IDisposableEntity>, IRegisterDisposable
    {
        private readonly ILogger _logger;
        private readonly IMediator _mediator;

        public RegisterDisposableBase(
            ILogger<RegisterDisposableBase> logger,
            IMediator mediator
        )
        {
            _logger = logger;
            _mediator = mediator;
        }

        public override async Task Run()
        {
            foreach (var entity in _entityList.ToList())
            {
                try
                {
                    await entity.Dispose();
                    await _mediator.Publish(
                        new EntityDisposedEvent(
                            entity
                        )
                    );
                }
                catch (Exception ex)
                {
                    _logger.LogError(
                        ex,
                        "Failed to Dispose of {Entity}",
                        entity
                    );
                }
            }
            _entityList.Clear();
        }
    }
}
