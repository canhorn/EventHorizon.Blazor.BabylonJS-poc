namespace EventHorizon.Game.Server.ServerModule.FeedbackMessage.Display
{
    using System;
    using EventHorizon.Observer.Model;
    using MediatR;

    public struct DisplayFeedbackMessageEvent
        : INotification
    {
        public string Message { get; }

        public DisplayFeedbackMessageEvent(
            string message
        )
        {
            Message = message;
        }
    }

    public interface DisplayFeedbackMessageEventObserver
        : ArgumentObserver<DisplayFeedbackMessageEvent>
    {
    }
}
