namespace EventHorizon.Observer.Unregister;

using System.Threading;
using System.Threading.Tasks;

using EventHorizon.Observer.State;

using MediatR;

public class UnregisterObserverCommandHandler
    : IRequestHandler<UnregisterObserverCommand>
{
    private readonly ObserverState _state;

    public UnregisterObserverCommandHandler(ObserverState state)
    {
        _state = state;
    }

    public Task<Unit> Handle(
        UnregisterObserverCommand request,
        CancellationToken cancellationToken
    )
    {
        request.NullCheck(nameof(request));
        _state.Remove(request.Observer);
        return Unit.Task;
    }
}
