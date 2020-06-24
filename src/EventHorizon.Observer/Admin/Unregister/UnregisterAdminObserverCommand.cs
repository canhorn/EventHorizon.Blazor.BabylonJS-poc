using EventHorizon.Observer.Admin.Model;
using MediatR;

namespace EventHorizon.Observer.Admin.Unregister
{
    public class UnregisterAdminObserverCommand : IRequest
    {
        public AdminObserverBase Observer { get; }

        public UnregisterAdminObserverCommand(
            AdminObserverBase observer
        )
        {
            Observer = observer;
        }
    }
}
