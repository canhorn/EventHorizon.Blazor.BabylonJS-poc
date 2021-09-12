namespace EventHorizon.Game.Editor.Client.Zone.ClientActions.Reload
{
    using System.Threading;
    using System.Threading.Tasks;

    using EventHorizon.Game.Editor.Zone.AdminClientAction.Api;
    using EventHorizon.Game.Editor.Zone.AdminClientAction.Attributes;
    using EventHorizon.Observer.Model;
    using EventHorizon.Observer.State;

    using MediatR;

    [AdminClientAction("EDITOR_SYSTEM_RELOADED_CLIENT_ACTION_EVENT")]
    public struct EditorSystemReloadedAdminClientAction
        : IAdminClientAction
    {
        public EditorSystemReloadedAdminClientAction(
            IAdminClientActionDataResolver _
        )
        {
        }
    }

    public interface EditorSystemReloadedAdminClientActionObserver
        : ArgumentObserver<EditorSystemReloadedAdminClientAction>
    {
    }

    public class EditorSystemReloadedAdminClientActionObserverHandler
        : INotificationHandler<EditorSystemReloadedAdminClientAction>
    {
        private readonly ObserverState _observer;

        public EditorSystemReloadedAdminClientActionObserverHandler(
            ObserverState observer
        )
        {
            _observer = observer;
        }

        public Task Handle(
            EditorSystemReloadedAdminClientAction notification,
            CancellationToken cancellationToken
        ) => _observer.Trigger<EditorSystemReloadedAdminClientActionObserver, EditorSystemReloadedAdminClientAction>(
            notification,
            cancellationToken
        );
    }
}
