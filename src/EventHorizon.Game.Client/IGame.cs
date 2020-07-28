namespace EventHorizon.Game.Client
{
    using System;
    using System.Threading.Tasks;
    using EventHorizon.Game.Client.Engine.Lifecycle.Model;
    using EventHorizon.Game.Client.Engine.Lifecycle.Register.Register;
    using EventHorizon.Game.Client.Engine.Lifecycle.Register.Unregister;
    using EventHorizon.Game.Client.Engine.Systems.Entity.Unregister;
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
            return _serviceProvider.GetRequiredService<T>();
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
