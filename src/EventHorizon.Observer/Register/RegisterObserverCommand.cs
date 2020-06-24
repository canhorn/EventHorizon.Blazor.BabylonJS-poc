using EventHorizon.Observer.Model;
using MediatR;

namespace EventHorizon.Observer.Register
{
    public class RegisterObserverCommand : IRequest
    {
        public ObserverBase Observer { get; }

        public RegisterObserverCommand(
            ObserverBase observer
        )
        {
            Observer = observer;
        }
    }
}
