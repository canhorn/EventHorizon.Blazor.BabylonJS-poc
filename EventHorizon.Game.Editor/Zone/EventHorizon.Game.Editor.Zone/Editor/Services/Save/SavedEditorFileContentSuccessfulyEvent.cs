namespace EventHorizon.Game.Editor.Zone.Editor.Services.Save;

using System.Threading;
using System.Threading.Tasks;
using EventHorizon.Observer.Model;
using EventHorizon.Observer.State;
using MediatR;

public struct SavedEditorFileContentSuccessfulyEvent : INotification { }

public interface SavedEditorFileContentSuccessfulyEventObserver
    : ArgumentObserver<SavedEditorFileContentSuccessfulyEvent> { }

public class SavedEditorFileContentSuccessfulyEventObserverHandler
    : INotificationHandler<SavedEditorFileContentSuccessfulyEvent>
{
    private readonly ObserverState _observer;

    public SavedEditorFileContentSuccessfulyEventObserverHandler(ObserverState observer)
    {
        _observer = observer;
    }

    public Task Handle(
        SavedEditorFileContentSuccessfulyEvent notification,
        CancellationToken cancellationToken
    ) =>
        _observer.Trigger<
            SavedEditorFileContentSuccessfulyEventObserver,
            SavedEditorFileContentSuccessfulyEvent
        >(notification, cancellationToken);
}
