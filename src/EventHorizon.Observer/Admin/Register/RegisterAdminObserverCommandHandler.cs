using EventHorizon.Observer.Admin.State;
using EventHorizon.Observer.State;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace EventHorizon.Observer.Admin.Register
{
    public class RegisterAdminObserverCommandHandler : IRequestHandler<RegisterAdminObserverCommand>
    {
        private readonly AdminObserverState _state;

        public RegisterAdminObserverCommandHandler(
            AdminObserverState state
        )
        {
            _state = state;
        }

        public Task<Unit> Handle(
            RegisterAdminObserverCommand request, 
            CancellationToken cancellationToken
        )
        {
            request.NullCheck(nameof(request));
            _state.RegisterAdminObserver(
                request.Observer
            );
            return Unit.Task;
        }
    }
}
