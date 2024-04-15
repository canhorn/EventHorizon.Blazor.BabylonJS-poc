namespace EventHorizon.Game.Editor.Client.Shared.Toast.Show;

using System.Threading;
using System.Threading.Tasks;
using EventHorizon.Game.Editor.Client.Shared.Toast.Model;
using EventHorizon.Observer.Model;
using EventHorizon.Observer.State;
using MediatR;

public class ShowMessageEvent : INotification
{
    public MessageModel Message { get; }

    public ShowMessageEvent(string header, string message, MessageLevel level = MessageLevel.Info)
    {
        Message = new MessageModel(header, message, level);
    }
}

public interface ShowMessageEventObserver : ArgumentObserver<ShowMessageEvent> { }

public class ShowMessageEventObserverHandler : INotificationHandler<ShowMessageEvent>
{
    private readonly ObserverState _observer;

    public ShowMessageEventObserverHandler(ObserverState observer)
    {
        _observer = observer;
    }

    public Task Handle(ShowMessageEvent notification, CancellationToken cancellationToken) =>
        _observer.Trigger<ShowMessageEventObserver, ShowMessageEvent>(
            notification,
            cancellationToken
        );
}
