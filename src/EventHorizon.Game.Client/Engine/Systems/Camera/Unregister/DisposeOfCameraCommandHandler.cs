namespace EventHorizon.Game.Client.Engine.Systems.Camera.Unregister;

using System;
using System.Threading;
using System.Threading.Tasks;

using EventHorizon.Game.Client.Core.Command.Model;
using EventHorizon.Game.Client.Engine.Systems.Camera.Api;

using MediatR;

public class DisposeOfCameraCommandHandler
    : IRequestHandler<DisposeOfCameraCommand, StandardCommandResult>
{
    private readonly ICameraState _state;

    public DisposeOfCameraCommandHandler(ICameraState state)
    {
        _state = state;
    }

    public Task<StandardCommandResult> Handle(
        DisposeOfCameraCommand request,
        CancellationToken cancellationToken
    )
    {
        _state.Dispose(request.Name);

        return new StandardCommandResult().FromResult();
    }
}
