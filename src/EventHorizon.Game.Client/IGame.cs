namespace EventHorizon.Game.Client
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Threading.Tasks;
    using EventHorizon.Game.Client.Core.Exceptions;
    using EventHorizon.Game.Client.Engine.Lifecycle.Model;
    using EventHorizon.Game.Client.Engine.Lifecycle.Register.Register;
    using EventHorizon.Game.Client.Engine.Lifecycle.Register.Unregister;
    using EventHorizon.Game.Client.Engine.Systems.Entity.Unregister;
    using EventHorizon.Observer.Model;
    using EventHorizon.Observer.State;
    using MediatR;
    using Microsoft.Extensions.DependencyInjection;

    public static class GameServiceProvider
    {
        private static IServiceProvider? _serviceProvider;
        public static void SetServiceProvider(
            IServiceProvider serviceProvider
        )
        {
            _serviceProvider = serviceProvider;
        }

        public static T GetService<T>()
        {
#if DEBUG
            if (_serviceProvider == null)
            {
                throw new GameRuntimeException(
                    "service_provider_not_set",
                    "Service provider was not set, make sure setup is complete."
                );
            }
#endif
            var service = _serviceProvider.GetRequiredService<T>();
#if DEBUG
            if (service == null)
            {
                throw new GameRuntimeException(
                    "service_not_registered",
                    $"Service was not registered. {nameof(T)}"
                );
            }
#endif
            return service;
        }

        public static T GetService__UNSAFE<T>()
        {
            return _serviceProvider.GetService<T>();
        }
    }

    public static class GamePlatfromServices
    {
        private static ObserverState? _observerState;
        public static void Setup()
        {
            _observerState = GameServiceProvider.GetService<ObserverState>();
        }

        public static void RegisterObserver(
            ObserverBase observer
        )
        {
            _observerState!.Register(
                observer
            );
        }
        public static void UnRegisterObserver(
            ObserverBase observer
        )
        {
            _observerState!.Remove(
                observer
            );
        }
    }

    public interface IGame
    {
        Task Setup();
        Task Initialize();
        Task Dispose();
        Task Start();
        Task Update();
    }

    public abstract class GameBase : IGame
    {
        protected readonly IMediator _mediator = GameServiceProvider.GetService<IMediator>();
        public async Task Register(
            LifecycleEntityBase entity
        )
        {
            await _mediator.Publish(
                new RegisterEntityEvent(
                    entity
                )
            );
        }
        public async Task Unregister(
            LifecycleEntityBase entity
        )
        {
            await _mediator.Publish(
                new UnregisterEntityEvent(
                    entity
                )
            );
        }
        public abstract Task Dispose();
        public abstract Task Initialize();
        public abstract Task Setup();
        public abstract Task Start();
        public abstract Task Update();
    }
}
