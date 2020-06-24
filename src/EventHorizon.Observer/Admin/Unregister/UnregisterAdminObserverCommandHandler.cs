using EventHorizon.Observer.Admin.State;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace EventHorizon.Observer.Admin.Unregister
{
    public class UnregisterAdminObserverCommandHandler : IRequestHandler<UnregisterAdminObserverCommand>
    {
        private readonly AdminObserverState _state;

        public UnregisterAdminObserverCommandHandler(
            AdminObserverState state
        )
        {
            _state = state;
        }

        public Task<Unit> Handle(
            UnregisterAdminObserverCommand request, 
            CancellationToken cancellationToken
        )
        {
            request.NullCheck(nameof(request));
            _state.RemoveAdminObserver(
                request.Observer
            );
            return Unit.Task;
        }
    }
}
