namespace EventHorizon.Game.Client.Engine.Lifecycle.Register.Unregister;

using System.Threading;
using System.Threading.Tasks;
using EventHorizon.Game.Client.Engine.Lifecycle.Register.Api;
using MediatR;

public class LifecycleUnregisterEntityEventHandler : INotificationHandler<UnregisterEntityEvent>
{
    private readonly IRegisterInitializable _registerInitializable;
    private readonly IRegisterDisposable _registerDisposable;
    private readonly IRegisterUpdatable _registerUpdatable;
    private readonly IRegisterDrawable _registerDrawable;

    public LifecycleUnregisterEntityEventHandler(
        IRegisterInitializable registerInitializable,
        IRegisterDisposable registerDisposable,
        IRegisterUpdatable registerUpdatable,
        IRegisterDrawable registerDrawable
    )
    {
        _registerInitializable = registerInitializable;
        _registerDisposable = registerDisposable;
        _registerUpdatable = registerUpdatable;
        _registerDrawable = registerDrawable;
    }

    public async Task Handle(
        UnregisterEntityEvent notification,
        CancellationToken cancellationToken
    )
    {
        await _registerInitializable.UnRegister(notification.Entity);
        await _registerDisposable.UnRegister(notification.Entity);
        await _registerUpdatable.UnRegister(notification.Entity);
        await _registerDrawable.UnRegister(notification.Entity);
    }
}
