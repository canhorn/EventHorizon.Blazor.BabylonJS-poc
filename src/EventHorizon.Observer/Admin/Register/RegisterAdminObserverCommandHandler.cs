namespace EventHorizon.Observer.Admin.Register;

using System.Threading;
using System.Threading.Tasks;

using EventHorizon.Observer.Admin.State;
using EventHorizon.Observer.State;

using MediatR;

public class RegisterAdminObserverCommandHandler
    : IRequestHandler<RegisterAdminObserverCommand>
{
    private readonly AdminObserverState _state;

    public RegisterAdminObserverCommandHandler(AdminObserverState state)
    {
        _state = state;
    }

    public Task Handle(
        RegisterAdminObserverCommand request,
        CancellationToken cancellationToken
    )
    {
        request.NullCheck(nameof(request));
        _state.RegisterAdminObserver(request.Observer);

        return Task.CompletedTask;
    }
}
