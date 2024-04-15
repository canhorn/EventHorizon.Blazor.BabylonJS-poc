namespace EventHorizon.Observer.Admin.Unregister;

using System.Threading;
using System.Threading.Tasks;
using EventHorizon.Observer.Admin.State;
using MediatR;

public class UnregisterAdminObserverCommandHandler : IRequestHandler<UnregisterAdminObserverCommand>
{
    private readonly AdminObserverState _state;

    public UnregisterAdminObserverCommandHandler(AdminObserverState state)
    {
        _state = state;
    }

    public Task Handle(UnregisterAdminObserverCommand request, CancellationToken cancellationToken)
    {
        request.NullCheck(nameof(request));
        _state.RemoveAdminObserver(request.Observer);

        return Task.CompletedTask;
    }
}
