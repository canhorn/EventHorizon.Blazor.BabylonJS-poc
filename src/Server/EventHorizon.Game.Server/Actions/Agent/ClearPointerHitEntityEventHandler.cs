namespace EventHorizon.Game.Server.Actions.Agent
{
    using System.Threading;
    using System.Threading.Tasks;
    using EventHorizon.Observer.State;
    using MediatR;

    public class ClearPointerHitEntityEventHandler
        : INotificationHandler<ClearPointerHitEntityEvent>
    {
        private readonly ObserverState _observer;

        public ClearPointerHitEntityEventHandler(
            ObserverState observer
        )
        {
            _observer = observer;
        }

        public Task Handle(
            ClearPointerHitEntityEvent notification,
            CancellationToken cancellationToken
        ) => _observer.Trigger<ClearPointerHitEntityEventObserver, ClearPointerHitEntityEvent>(
            notification,
            cancellationToken
        );
    }
}
