namespace EventHorizon.Blazor.BabylonJS.Authentication.Set
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using EventHorizon.Observer.Model;
    using EventHorizon.Observer.State;
    using MediatR;

    public struct AccessTokenSetEvent
        : INotification
    {
        public string AccessToken { get; }

        public AccessTokenSetEvent(
            string accessToken
        )
        {
            AccessToken = accessToken;
        }
    }

    public interface AccessTokenSetEventObserver
        : ArgumentObserver<AccessTokenSetEvent>
    {
    }

    public class AccessTokenSetEventObserverHandler
        : INotificationHandler<AccessTokenSetEvent>
    {
        private readonly ObserverState _observer;

        public AccessTokenSetEventObserverHandler(
            ObserverState observer
        )
        {
            _observer = observer;
        }

        public Task Handle(
            AccessTokenSetEvent notification,
            CancellationToken cancellationToken
        ) => _observer.Trigger<AccessTokenSetEventObserver, AccessTokenSetEvent>(
            notification,
            cancellationToken
        );
    }
}
