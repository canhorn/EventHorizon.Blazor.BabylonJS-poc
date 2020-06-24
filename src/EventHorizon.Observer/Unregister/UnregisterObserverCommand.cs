using EventHorizon.Observer.Model;
using MediatR;

namespace EventHorizon.Observer.Unregister
{
    public class UnregisterObserverCommand : IRequest
    {
        public ObserverBase Observer { get; }

        public UnregisterObserverCommand(
            ObserverBase observer
        )
        {
            Observer = observer;
        }
    }
}
