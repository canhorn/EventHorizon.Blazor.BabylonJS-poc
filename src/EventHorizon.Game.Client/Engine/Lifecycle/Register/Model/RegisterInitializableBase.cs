namespace EventHorizon.Game.Client.Engine.Lifecycle.Register.Model
{
    using System;
    using System.Threading.Tasks;
    using EventHorizon.Game.Client.Core.Timer.Api;
    using EventHorizon.Game.Client.Engine.Lifecycle.Api;
    using EventHorizon.Game.Client.Engine.Lifecycle.Register.Api;
    using EventHorizon.Game.Client.Engine.Lifecycle.Register.Initialized;
    using MediatR;

    public class RegisterInitializableBase
        : RegisterBase<IInitializableEntity>, IRegisterInitializable
    {
        private readonly IMediator _mediator;
        private readonly ITimerService _timerService;

        public RegisterInitializableBase(
            IMediator mediator,
            ITimerService timerService
        )
        {
            _mediator = mediator;
            _timerService = timerService;
        }

        private void HandleRun()
        {
            Run().ConfigureAwait(false).GetAwaiter().GetResult();
        }

        public override async Task Run()
        {
            foreach (var entity in _entityList)
            {
                await entity.Initialize();
                await entity.PostInitialize();
                await _mediator.Publish(
                    new EntityInitializedEvent(
                        entity
                    )
                );
            }
            _entityList.Clear();
            _timerService.SetTimer(100, HandleRun);
        }

        public override Task CleanUp()
        {
            _timerService.Clear();
            return base.CleanUp();
        }
    }
}
