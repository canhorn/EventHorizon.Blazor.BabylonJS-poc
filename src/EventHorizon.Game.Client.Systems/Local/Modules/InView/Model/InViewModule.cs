namespace EventHorizon.Game.Client.Systems.Local.Modules.InView.Model
{
    using System;
    using System.Threading.Tasks;
    using EventHorizon.Game.Client.Core.Factory.Api;
    using EventHorizon.Game.Client.Core.Timer.Api;
    using EventHorizon.Game.Client.Engine.Rendering.Api;
    using EventHorizon.Game.Client.Engine.Systems.Entity.Api;
    using EventHorizon.Game.Client.Engine.Systems.Module.Model;
    using EventHorizon.Game.Client.Systems.Entity.Instanced.Model;
    using EventHorizon.Game.Client.Systems.Local.Modules.InView.Api;
    using EventHorizon.Game.Client.Systems.Local.Modules.InView.Entering;
    using EventHorizon.Game.Client.Systems.Local.Modules.InView.Exiting;
    using EventHorizon.Game.Client.Systems.Local.Modules.MeshManagement.Api;
    using MediatR;

    public class InViewModule
        : ModuleEntityBase,
        IInViewModule
    {
        private readonly IRenderingScene _renderingScene;
        private readonly IObjectEntity _entity;
        private readonly IMeshModule _meshModule;
        private readonly IMediator _mediator;
        private readonly IIntervalTimerService _checkForInViewIntervalTimer;
        private readonly IIntervalTimerService _toggleInViewViewIntervalTimer;

        private bool _lastInView;

        public override int Priority => 0;

        public InViewModule(
            IObjectEntity entity
        )
        {
            _entity = entity;
            _meshModule = entity.GetModule<IMeshModule>(IMeshModule.MODULE_NAME);

            _mediator = GameServiceProvider.GetService<IMediator>();
            _renderingScene = GameServiceProvider.GetService<IRenderingScene>();

            var factory = GameServiceProvider.GetService<IFactory<IIntervalTimerService>>();
            _checkForInViewIntervalTimer = factory.Create();
            _checkForInViewIntervalTimer.Setup(
                100,
                HandleCheckForEntityInView
            );
            _checkForInViewIntervalTimer.Start();

            _toggleInViewViewIntervalTimer = factory.Create();
            _toggleInViewViewIntervalTimer.Setup(
                5000, // 5 seconds
                HandleToggleInView
            );
            _toggleInViewViewIntervalTimer.Start();
        }

        public override Task Dispose()
        {
            _checkForInViewIntervalTimer.Dispose();
            _toggleInViewViewIntervalTimer.Dispose();
            return Task.CompletedTask;
        }

        public override Task Initialize()
        {
            return Task.CompletedTask;
        }

        public override Task Update()
        {
            return Task.CompletedTask;
        }

        private async Task HandleCheckForEntityInView()
        {
            // Check activeCamera.IsInFrustrum
            var isInView = _renderingScene.ActiveCamera.IsInFrustum(
                _meshModule.Mesh
            );
            if (isInView == _lastInView)
            {
                return;
            }
            // Publish Events based on IsInView
            var clientId = _entity.ClientId;
            if (isInView)
            {
                await _mediator.Publish(
                    new EntityEnteringViewEvent(
                        clientId
                    )
                );
            }
            else
            {
                await _mediator.Publish(
                    new EntityExitingViewEvent(
                        clientId
                    )
                );
            }
            _lastInView = isInView;
        }

        private Task HandleToggleInView()
        {
            // TODO: [PERFORMANCE] : Check into this and see if it is still necessary.
            _lastInView = !_lastInView;
            return Task.CompletedTask;
        }
    }
}
