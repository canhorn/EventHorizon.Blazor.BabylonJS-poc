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

    public class RegisterInitializableBase
        : RegisterBase<IInitializableEntity>, IRegisterInitializable
    {
        private readonly IMediator _mediator;
        private readonly ITimerService _timerService;

        public RegisterInitializableBase(
            IMediator mediator,
            IFactory<ITimerService> timerServiceFactory
        )
        {
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
                await entity.Initialize();
            }
            foreach (var entity in list)
            {
                await entity.PostInitialize();
                await _mediator.Publish(
                    new EntityInitializedEvent(
                        entity
                    )
                );
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
