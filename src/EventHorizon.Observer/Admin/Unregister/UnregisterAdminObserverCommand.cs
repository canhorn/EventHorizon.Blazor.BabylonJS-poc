namespace EventHorizon.Observer.Admin.Unregister;

using EventHorizon.Observer.Admin.Model;

using MediatR;

public class UnregisterAdminObserverCommand : IRequest
{
    public AdminObserverBase Observer { get; }

    public UnregisterAdminObserverCommand(AdminObserverBase observer)
    {
        Observer = observer;
    }
}
