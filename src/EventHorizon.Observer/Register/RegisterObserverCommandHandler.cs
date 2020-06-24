using EventHorizon.Observer.State;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace EventHorizon.Observer.Register
{
    public class RegisterObserverCommandHandler : IRequestHandler<RegisterObserverCommand>
    {
        private readonly ObserverState _state;

        public RegisterObserverCommandHandler(
            ObserverState state
        )
        {
            _state = state;
        }

        public Task<Unit> Handle(
            RegisterObserverCommand request, 
            CancellationToken cancellationToken
        )
        {
            request.NullCheck(nameof(request));
            _state.Register(
                request.Observer
            );
            return Unit.Task;
        }
    }
}
