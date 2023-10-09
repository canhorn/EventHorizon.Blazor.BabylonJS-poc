namespace EventHorizon.Game.Client.Systems.ServerModule.Dispose;

using System;
using System.Threading;
using System.Threading.Tasks;

using EventHorizon.Game.Client.Core.Command.Model;
using EventHorizon.Game.Client.Engine.Lifecycle.Register.Api;
using EventHorizon.Game.Client.Systems.ServerModule.Api;

using MediatR;

public class DisposeOfServerModuleCommandHandler
    : IRequestHandler<DisposeOfServerModuleCommand, StandardCommandResult>
{
    private readonly ServerModuleState _state;
    private readonly IRegisterInitializable _registerInitializable;
    private readonly IRegisterUpdatable _registerUpdatable;
    private readonly IRegisterDisposable _registerDisposable;

    public DisposeOfServerModuleCommandHandler(
        ServerModuleState state,
        IRegisterInitializable registerInitializable,
        IRegisterUpdatable registerUpdatable,
        IRegisterDisposable registerDisposable
    )
    {
        _state = state;
        _registerInitializable = registerInitializable;
        _registerUpdatable = registerUpdatable;
        _registerDisposable = registerDisposable;
    }

    public Task<StandardCommandResult> Handle(
        DisposeOfServerModuleCommand request,
        CancellationToken cancellationToken
    )
    {
        var serverModule = _state.Remove(request.Name);

        if (serverModule.HasValue)
        {
            _registerInitializable.UnRegister(serverModule.Value);
            _registerUpdatable.UnRegister(serverModule.Value);
            _registerDisposable.UnRegister(serverModule.Value);
            serverModule.Value.Dispose();
        }

        return new StandardCommandResult().FromResult();
    }
}
