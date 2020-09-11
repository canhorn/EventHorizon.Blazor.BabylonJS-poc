namespace EventHorizon.Game.Client
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Threading.Tasks;
    using EventHorizon.Game.Client.Core.Exceptions;
    using EventHorizon.Game.Client.Engine.Lifecycle.Model;
    using EventHorizon.Game.Client.Engine.Lifecycle.Register.Register;
    using EventHorizon.Game.Client.Engine.Lifecycle.Register.Unregister;
    using EventHorizon.Observer.Model;
    using EventHorizon.Observer.State;
    using MediatR;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Logging;

    public static class GameServiceProvider
    {
        private static IServiceProvider? _serviceProvider;
        public static void SetServiceProvider(
            IServiceProvider serviceProvider
        )
        {
            _serviceProvider = serviceProvider;
        }

        public static T GetService<T>() where T : notnull
        {
#if DEBUG
            if (_serviceProvider.IsNull())
            {
                throw new GameRuntimeException(
                    "service_provider_not_set",
                    "Service provider was not set, make sure setup is complete."
                );
            }
#endif
            return _serviceProvider.GetRequiredService<T>();
        }

        [return: MaybeNull]
        public static T GetService__UNSAFE<T>()
        {
            if (_serviceProvider.IsNull())
            {
                return default;
            }
            return _serviceProvider.GetService<T>();
        }
    }

    public static class GamePlatfrom
    {
        private static ObserverState? _observerState;
        internal static void Setup()
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
        public static ILogger Logger<T>()
        {
            return GameServiceProvider.GetService<ILogger<T>>();
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
