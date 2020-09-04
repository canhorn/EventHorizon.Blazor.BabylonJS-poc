namespace EventHorizon.Game.Server.SkillSelection.Activated
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using EventHorizon.Observer.Model;
    using EventHorizon.Observer.State;
    using MediatR;

    public struct SkillSelectionGuiActivatedEvent
        : INotification
    {
    }

    public interface SkillSelectionGuiActivatedEventObserver
        : ArgumentObserver<SkillSelectionGuiActivatedEvent>
    {
    }

    public class SkillSelectionGuiActivatedEventHandler
        : INotificationHandler<SkillSelectionGuiActivatedEvent>
    {
        private readonly ObserverState _observer;

        public SkillSelectionGuiActivatedEventHandler(
            ObserverState observer
        )
        {
            _observer = observer;
        }

        public Task Handle(
            SkillSelectionGuiActivatedEvent notification,
            CancellationToken cancellationToken
        ) => _observer.Trigger<SkillSelectionGuiActivatedEventObserver, SkillSelectionGuiActivatedEvent>(
            notification,
            cancellationToken
        );
    }
}
