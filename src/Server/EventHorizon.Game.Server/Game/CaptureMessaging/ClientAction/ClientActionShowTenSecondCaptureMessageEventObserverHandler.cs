namespace EventHorizon.Game.Server.Game.CaptureMessaging.ClientAction.Show
{
    using System.Threading;
    using System.Threading.Tasks;
    using EventHorizon.Observer.State;
    using MediatR;

    public class ClientActionShowTenSecondCaptureMessageEventObserverHandler
        : INotificationHandler<ClientActionShowTenSecondCaptureMessageEvent>
    {
        private readonly ObserverState _observer;

        public ClientActionShowTenSecondCaptureMessageEventObserverHandler(
            ObserverState observer
        )
        {
            _observer = observer;
        }

        public Task Handle(
            ClientActionShowTenSecondCaptureMessageEvent notification,
            CancellationToken cancellationToken
        ) => _observer.Trigger<ClientActionShowTenSecondCaptureMessageEventObserver, ClientActionShowTenSecondCaptureMessageEvent>(
            notification,
            cancellationToken
        );
    }
}
