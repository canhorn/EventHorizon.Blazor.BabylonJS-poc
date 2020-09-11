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
        : RegisterBase<IInitializableEntity>, IRegisterInitializable
    {
        private readonly ILogger _logger;
        private readonly IMediator _mediator;
        private readonly ITimerService _timerService;

        public RegisterInitializableBase(
            ILogger<RegisterInitializableBase> logger,
            IMediator mediator,
            IFactory<ITimerService> timerServiceFactory
        )
        {
            _logger = logger;
            _mediator = mediator;
            _timerService = timerServiceFactory.Create();
        }

        private void HandleRun()
        {
            Run().ConfigureAwait(false).GetAwaiter().GetResult();
        }

        public override async Task Run()
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
            _timerService.SetTimer(100, HandleRun);
        }

        public override Task CleanUp()
        {
            _timerService.Clear();
            return base.CleanUp();
        }
    }
}
