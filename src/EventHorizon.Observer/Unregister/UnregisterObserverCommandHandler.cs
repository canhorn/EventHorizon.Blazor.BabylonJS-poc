using EventHorizon.Observer.State;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace EventHorizon.Observer.Unregister
{
    public class UnregisterObserverCommandHandler : IRequestHandler<UnregisterObserverCommand>
    {
        private readonly ObserverState _state;

        public UnregisterObserverCommandHandler(
            ObserverState state
        )
        {
            _state = state;
        }

        public Task<Unit> Handle(
            UnregisterObserverCommand request, 
            CancellationToken cancellationToken
        )
        {
            request.NullCheck(nameof(request));
            _state.Remove(
                request.Observer
            );
            return Unit.Task;
        }
    }
}
