namespace EventHorizon.Game.Server.ServerModule.FeedbackMessage.Display
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using EventHorizon.Observer.Model;
    using EventHorizon.Observer.State;
    using MediatR;

    public struct DisplayFeedbackMessageEvent : INotification
    {
        public string Message { get; }

        public DisplayFeedbackMessageEvent(string message)
        {
            Message = message;
        }
    }

    public interface DisplayFeedbackMessageEventObserver
        : ArgumentObserver<DisplayFeedbackMessageEvent> { }

    // TODO: Move this into an Implementation Project, Remove from the SDK
    public class DisplayFeedbackMessageEventHandler
        : INotificationHandler<DisplayFeedbackMessageEvent>
    {
        private readonly ObserverState _observer;

        public DisplayFeedbackMessageEventHandler(ObserverState observer)
        {
            _observer = observer;
        }

        public Task Handle(
            DisplayFeedbackMessageEvent notification,
            CancellationToken cancellationToken
        ) =>
            _observer.Trigger<
                DisplayFeedbackMessageEventObserver,
                DisplayFeedbackMessageEvent
            >(notification, cancellationToken);
    }
}
