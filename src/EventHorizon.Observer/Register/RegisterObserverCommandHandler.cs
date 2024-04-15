namespace EventHorizon.Observer.Register;

using System.Threading;
using System.Threading.Tasks;
using EventHorizon.Observer.State;
using MediatR;

public class RegisterObserverCommandHandler : IRequestHandler<RegisterObserverCommand>
{
    private readonly ObserverState _state;

    public RegisterObserverCommandHandler(ObserverState state)
    {
        _state = state;
    }

    public Task Handle(RegisterObserverCommand request, CancellationToken cancellationToken)
    {
        request.NullCheck(nameof(request));
        _state.Register(request.Observer);

        return Task.CompletedTask;
    }
}
