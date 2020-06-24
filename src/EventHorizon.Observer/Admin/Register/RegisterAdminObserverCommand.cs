using EventHorizon.Observer.Admin.Model;
using MediatR;

namespace EventHorizon.Observer.Admin.Register
{
    public class RegisterAdminObserverCommand : IRequest
    {
        public AdminObserverBase Observer { get; }

        public RegisterAdminObserverCommand(
            AdminObserverBase observer
        )
        {
            Observer = observer;
        }
    }
}
