namespace EventHorizon.Observer.Register;

using EventHorizon.Observer.Model;
using MediatR;

public class RegisterObserverCommand : IRequest
{
    public ObserverBase Observer { get; }

    public RegisterObserverCommand(ObserverBase observer)
    {
        Observer = observer;
    }
}
