namespace EventHorizon.Game.Client.Engine.Gui.Changed
{
    using System.Threading;
    using System.Threading.Tasks;
    using EventHorizon.Observer.Model;
    using EventHorizon.Observer.State;
    using MediatR;

    public struct GuiLayoutDataChangedEvent
        : INotification
    {
        public string Id { get; }

        public GuiLayoutDataChangedEvent(
            string id
        )
        {
            Id = id;
        }
    }

    public interface GuiLayoutDataChangedEventObserver
        : ArgumentObserver<GuiLayoutDataChangedEvent>
    {
    }

    public class GuiLayoutDataChangedEventHandler
        : INotificationHandler<GuiLayoutDataChangedEvent>
    {
        private readonly ObserverState _observer;

        public GuiLayoutDataChangedEventHandler(
            ObserverState observer
        )
        {
            _observer = observer;
        }

        public Task Handle(
            GuiLayoutDataChangedEvent notification,
            CancellationToken cancellationToken
        ) => _observer.Trigger<GuiLayoutDataChangedEventObserver, GuiLayoutDataChangedEvent>(
            notification,
            cancellationToken
        );
    }
}
