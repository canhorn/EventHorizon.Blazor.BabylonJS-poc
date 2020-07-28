namespace EventHorizon.Game.Client.Engine.Lifecycle.Register.Register
{
    using System.Threading;
    using System.Threading.Tasks;
    using EventHorizon.Game.Client.Engine.Lifecycle.Api;
    using EventHorizon.Observer.Model;
    using EventHorizon.Observer.State;
    using MediatR;

    public struct RegisterEntityEvent 
        : INotification
    {
        public ILifecycleEntity Entity { get; }

        public RegisterEntityEvent(
            ILifecycleEntity entity
        )
        {
            Entity = entity;
        }
    }

    public interface RegisterEntityEventObserver
        : ArgumentObserver<RegisterEntityEvent>
    {
    }

    public class RegisterEntityEventHandler
        : INotificationHandler<RegisterEntityEvent>
    {
        private readonly ObserverState _observer;

        public RegisterEntityEventHandler(
            ObserverState observer
        )
        {
            _observer = observer;
        }

        public Task Handle(
            RegisterEntityEvent notification,
            CancellationToken cancellationToken
        ) => _observer.Trigger<RegisterEntityEventObserver, RegisterEntityEvent>(
            notification,
            cancellationToken
        );
    }
}
