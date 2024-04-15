namespace EventHorizon.Observer.Admin.Register;

using EventHorizon.Observer.Admin.Model;
using MediatR;

public class RegisterAdminObserverCommand : IRequest
{
    public AdminObserverBase Observer { get; }

    public RegisterAdminObserverCommand(AdminObserverBase observer)
    {
        Observer = observer;
    }
}
