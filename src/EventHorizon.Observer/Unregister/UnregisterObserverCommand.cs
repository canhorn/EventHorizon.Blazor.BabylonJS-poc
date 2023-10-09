namespace EventHorizon.Observer.Unregister;

using EventHorizon.Observer.Model;

using MediatR;

public class UnregisterObserverCommand : IRequest
{
    public ObserverBase Observer { get; }

    public UnregisterObserverCommand(ObserverBase observer)
    {
        Observer = observer;
    }
}
