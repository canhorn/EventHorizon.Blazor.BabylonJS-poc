namespace EventHorizon.Game.Client.Engine.Lifecycle.Register.Register;

using System.Threading;
using System.Threading.Tasks;
using EventHorizon.Game.Client.Engine.Lifecycle.Register.Api;
using MediatR;

public class LifecycleRegisterEntityEventHandler : INotificationHandler<RegisterEntityEvent>
{
    private readonly IRegisterInitializable _registerInitializable;
    private readonly IRegisterDisposable _registerDisposable;

    public LifecycleRegisterEntityEventHandler(
        IRegisterInitializable registerInitializable,
        IRegisterDisposable registerDisposable
    )
    {
        _registerInitializable = registerInitializable;
        _registerDisposable = registerDisposable;
    }

    public async Task Handle(RegisterEntityEvent notification, CancellationToken cancellationToken)
    {
        await _registerInitializable.Register(notification.Entity);
        await _registerDisposable.Register(notification.Entity);
    }
}
