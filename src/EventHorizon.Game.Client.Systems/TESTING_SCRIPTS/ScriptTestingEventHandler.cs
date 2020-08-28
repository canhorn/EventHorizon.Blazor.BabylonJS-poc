namespace EventHorizon.Game.Client.Systems.TESTING_SCRIPTS
{
    using System.Threading;
    using System.Threading.Tasks;
    using EventHorizon.Game.Client.Engine.Events.Testing;
    using EventHorizon.Observer.State;
    using MediatR;

    public class ScriptTestingEventHandler
        : INotificationHandler<ScriptTestingEvent>
    {
        private readonly ObserverState _observer;

        public ScriptTestingEventHandler(
            ObserverState observer
        )
        {
            _observer = observer;
        }

        public Task Handle(
            ScriptTestingEvent notification,
            CancellationToken cancellationToken
        ) => _observer.Trigger<ScriptTestingEventObserver, ScriptTestingEvent>(
            notification,
            cancellationToken
        );
    }
}