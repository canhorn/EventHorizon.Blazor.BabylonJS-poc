namespace EventHorizon.Game.Client.Systems.Entity.Modules.Animation.Model
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using EventHorizon.Game.Client.Core.Factory.Api;
    using EventHorizon.Game.Client.Core.Timer.Api;
    using EventHorizon.Game.Client.Engine.Systems.Entity.Api;
    using EventHorizon.Game.Client.Engine.Systems.Module.Model;
    using EventHorizon.Game.Client.Systems.Entity.Actions;
    using EventHorizon.Game.Client.Systems.Entity.Modules.Animation.Api;
    using EventHorizon.Game.Client.Systems.Entity.Modules.Animation.Loaded;
    using EventHorizon.Game.Client.Systems.Entity.Modules.Animation.Play;
    using EventHorizon.Game.Client.Systems.Entity.Moving;
    using EventHorizon.Game.Client.Systems.Entity.Stopping;
    using EventHorizon.Game.Client.Systems.Local.InView.Entering;
    using EventHorizon.Game.Client.Systems.Local.InView.Exiting;
    using EventHorizon.Observer.Register;
    using EventHorizon.Observer.Unregister;
    using MediatR;

    public class AnimationModule
        : ModuleEntityBase,
        IAnimationModule,
        PlayAnimationEventObserver,
        AnimationListLoadedEventObserver,
        EntityEnteringViewEventObserver,
        EntityExitingViewEventObserver,
        EntityMovingEventObserver,
        EntityStoppingEventObserver
    {
        private bool _enabled = true;
        private string _currentAnimation = "__invalid__";
        private string _setMovementAnimation = AnimationConstants.DEFAULT_ANIMATION;

        private readonly IDictionary<string, IAnimationGroup> _animationList = new Dictionary<string, IAnimationGroup>();
        private readonly IMediator _mediator;
        private readonly IIntervalTimerService _intervalTimer;
        private readonly IObjectEntity _entity;

        public override int Priority => 0;

        public AnimationModule(
            IObjectEntity entity
        )
        {
            _mediator = GameServiceProvider.GetService<IMediator>();
            _intervalTimer = GameServiceProvider.GetService<IFactory<IIntervalTimerService>>().Create();
            _entity = entity;
        }

        public override async Task Initialize()
        {
            _intervalTimer.Setup(
                100,
                HandleOnCheckMovement
            );
            _intervalTimer.Start();

            await _mediator.Send(
                new RegisterObserverCommand(
                    this
                )
            );
        }

        public override async Task Dispose()
        {
            _intervalTimer.Dispose();
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

        public Task Handle(
            AnimationListLoadedEvent args
        )
        {
            if (_entity.ClientId != args.ClientId)
            {
                return Task.CompletedTask;
            }
            _animationList.Clear();
            foreach (var animation in args.AnimationList)
            {
                _animationList.Add(
                    animation.Name,
                    animation
                );
                animation.Pause();
            }

            return Task.CompletedTask;
        }

        public Task Handle(
            PlayAnimationEvent args
        )
        {
            if (
                _entity.ClientId != args.ClientId ||
                _currentAnimation == args.Animation
            )
            {
                return Task.CompletedTask;
            }
            if (_animationList.TryGetValue(
                _currentAnimation,
                out var currentAnimation
            ))
            {
                currentAnimation.Pause();
            }
            if (_animationList.TryGetValue(
                args.Animation,
                out var nextAnimation
            ))
            {
                if (_enabled)
                {
                    nextAnimation.Play(true);
                }
                _currentAnimation = args.Animation;
            }
            return Task.CompletedTask;
        }

        public Task Handle(
            EntityExitingViewEvent args
        )
        {
            if (_entity.ClientId != args.ClientId
                || !_enabled)
            {
                return Task.CompletedTask;
            }

            if (_animationList.TryGetValue(
                _currentAnimation,
                out var currentAnimation
            ))
            {
                currentAnimation.Pause();
            }
            _enabled = false;

            return Task.CompletedTask;
        }

        public Task Handle(
            EntityEnteringViewEvent args
        )
        {
            if (_entity.ClientId != args.ClientId
                || _enabled)
            {
                return Task.CompletedTask;
            }

            if (_animationList.TryGetValue(
                _currentAnimation,
                out var currentAnimation
            ))
            {
                currentAnimation.Play(true);
            }
            _enabled = true;

            return Task.CompletedTask;
        }

        public Task Handle(
            EntityMovingEvent args
        )
        {
            if (_entity.ClientId != args.ClientId)
            {
                return Task.CompletedTask;
            }

            _setMovementAnimation = AnimationConstants.RUNNING_ANIMATION;
            return Task.CompletedTask;
        }

        private async Task HandleOnCheckMovement()
        {
            if (_setMovementAnimation == _currentAnimation)
            {
                return;
            }
            await Handle(
                new PlayAnimationEvent(
                    _entity.ClientId,
                    animation: _setMovementAnimation
                )
            );
        }

        public Task Handle(
            EntityStoppingEvent args
        )
        {
            if (_entity.ClientId != args.ClientId)
            {
                return Task.CompletedTask;
            }

            _setMovementAnimation = AnimationConstants.DEFAULT_ANIMATION;

            return Task.CompletedTask;
        }
    }
}
