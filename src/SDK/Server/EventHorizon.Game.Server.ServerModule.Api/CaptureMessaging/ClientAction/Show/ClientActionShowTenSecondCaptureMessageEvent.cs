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
    //[ClientAction("Server.SHOW_TEN_SECOND_CAPTURE_MESSAGE")]
    public struct ClientActionShowTenSecondCaptureMessageEvent
        : INotification,
        IClientAction
    {
        public ClientActionShowTenSecondCaptureMessageEvent(
            IClientActionDataResolver _
        )
        {
        }

    }

    public interface ClientActionShowTenSecondCaptureMessageEventObserver
        : ArgumentObserver<ClientActionShowTenSecondCaptureMessageEvent>
    {
    }

    // TODO: Move this into an Implementation Project, Remove from the SDK
    public class ClientActionShowTenSecondCaptureMessageEventHandler
        : INotificationHandler<ClientActionShowTenSecondCaptureMessageEvent>
    {
        private readonly ObserverState _observer;

        public ClientActionShowTenSecondCaptureMessageEventHandler(
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
