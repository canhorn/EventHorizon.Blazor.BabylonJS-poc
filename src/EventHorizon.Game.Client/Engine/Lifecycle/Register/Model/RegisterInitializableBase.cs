namespace EventHorizon.Game.Client.Engine.Lifecycle.Register.Model
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using EventHorizon.Game.Client.Core.Factory.Api;
    using EventHorizon.Game.Client.Core.Timer.Api;
    using EventHorizon.Game.Client.Engine.Lifecycle.Api;
    using EventHorizon.Game.Client.Engine.Lifecycle.Register.Api;
    using EventHorizon.Game.Client.Engine.Lifecycle.Register.Initialized;
    using MediatR;
    using Microsoft.Extensions.Logging;

    public class RegisterInitializableBase
        : RegisterBase<IInitializableEntity>, 
        IRegisterInitializable
    {
        private readonly ILogger _logger;
        private readonly IMediator _mediator;
        private readonly IIntervalTimerService _intervalTimer;

        public RegisterInitializableBase(
            ILogger<RegisterInitializableBase> logger,
            IMediator mediator,
            IFactory<IIntervalTimerService> intervalTimerFactory
        )
        {
            _logger = logger;
            _mediator = mediator;
            _intervalTimer = intervalTimerFactory.Create();
            _intervalTimer.Setup(
                100, 
                HandleRun
            );
        }

        private async Task HandleRun()
        {
            var list = _entityList.ToList();
            _entityList.Clear();
            foreach (var entity in list)
            {
                try
                {
                    await entity.Initialize();
                }
                catch (Exception ex)
                {
                    _logger.LogError(
                        ex,
                        "Failed to Initialize Entity: {ClientId}",
                        entity.ClientId
                    );
                }
            }
            foreach (var entity in list)
            {
                try
                {
                    await entity.PostInitialize();
                    await _mediator.Publish(
                        new EntityInitializedEvent(
                            entity
                        )
                    );
                }
                catch (Exception ex)
                {
                    _logger.LogError(
                        ex,
                        "Failed to PostInitialize Entity: {ClientId}",
                        entity.ClientId
                    );
                }
            }
        }

        public override Task Run()
        {
            _intervalTimer.Start();

            return Task.CompletedTask;
        }

        public override Task CleanUp()
        {
            _intervalTimer.Pause();
            _entityList.Clear();
            return base.CleanUp();
        }
    }
}
