namespace EventHorizon.Game.Server.ServerModule.FeedbackMessage.Display;

using System.Threading;
using System.Threading.Tasks;
using EventHorizon.Observer.State;
using MediatR;

// TODO: Move this into an Implementation Project, Remove from the SDK
public class DisplayFeedbackMessageEventHandler : INotificationHandler<DisplayFeedbackMessageEvent>
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
        _observer.Trigger<DisplayFeedbackMessageEventObserver, DisplayFeedbackMessageEvent>(
            notification,
            cancellationToken
        );
}
