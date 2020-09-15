namespace EventHorizon.Game.Server.Game.CaptureMessaging.ClientAction.Show
{
    using System.Threading;
    using System.Threading.Tasks;
    using EventHorizon.Observer.State;
    using MediatR;

    public class ClientActionShowFiveSecondCaptureMessageEventObserverHandler
        : INotificationHandler<ClientActionShowFiveSecondCaptureMessageEvent>
    {
        private readonly ObserverState _observer;

        public ClientActionShowFiveSecondCaptureMessageEventObserverHandler(
            ObserverState observer
        )
        {
            _observer = observer;
        }

        public Task Handle(
            ClientActionShowFiveSecondCaptureMessageEvent notification,
            CancellationToken cancellationToken
        ) => _observer.Trigger<ClientActionShowFiveSecondCaptureMessageEventObserver, ClientActionShowFiveSecondCaptureMessageEvent>(
            notification,
            cancellationToken
        );
    }
}
