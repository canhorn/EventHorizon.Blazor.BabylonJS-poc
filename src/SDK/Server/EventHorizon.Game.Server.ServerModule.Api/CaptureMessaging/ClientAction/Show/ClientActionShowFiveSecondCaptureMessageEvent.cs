namespace EventHorizon.Game.Server.ServerModule.CaptureMessaging.ClientAction.Show
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using EventHorizon.Game.Client.Engine.Systems.ClientAction.Api;
    using EventHorizon.Observer.Model;
    using EventHorizon.Observer.State;
    using MediatR;

    // TODO: [ClientAction] : Finish Implementation
    //[ClientAction("Server.SHOW_FIVE_SECOND_CAPTURE_MESSAGE")]
    public struct ClientActionShowFiveSecondCaptureMessageEvent : INotification, IClientAction
    {
        public ClientActionShowFiveSecondCaptureMessageEvent(IClientActionDataResolver _) { }
    }

    public interface ClientActionShowFiveSecondCaptureMessageEventObserver
        : ArgumentObserver<ClientActionShowFiveSecondCaptureMessageEvent> { }

    // TODO: Move this into an Implementation Project, Remove from the SDK
    public class ShowFiveSecondCaptureMessageEventHandler
        : INotificationHandler<ClientActionShowFiveSecondCaptureMessageEvent>
    {
        private readonly ObserverState _observer;

        public ShowFiveSecondCaptureMessageEventHandler(ObserverState observer)
        {
            _observer = observer;
        }

        public Task Handle(
            ClientActionShowFiveSecondCaptureMessageEvent notification,
            CancellationToken cancellationToken
        ) =>
            _observer.Trigger<
                ClientActionShowFiveSecondCaptureMessageEventObserver,
                ClientActionShowFiveSecondCaptureMessageEvent
            >(notification, cancellationToken);
    }
}
