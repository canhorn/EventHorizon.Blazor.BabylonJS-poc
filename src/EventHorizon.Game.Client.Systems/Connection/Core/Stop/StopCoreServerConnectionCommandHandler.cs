namespace EventHorizon.Game.Client.Systems.Connection.Core.Stop;

using System;
using System.Threading;
using System.Threading.Tasks;

using EventHorizon.Game.Client.Core.Command.Model;
using EventHorizon.Game.Client.Systems.Connection.Core.Api;

using MediatR;

using Newtonsoft.Json.Serialization;

public class StopCoreServerConnectionCommandHandler
    : IRequestHandler<StopCoreServerConnectionCommand, StandardCommandResult>
{
    private readonly CoreConnectionState _state;

    public StopCoreServerConnectionCommandHandler(CoreConnectionState state)
    {
        _state = state;
    }

    public async Task<StandardCommandResult> Handle(
        StopCoreServerConnectionCommand request,
        CancellationToken cancellationToken
    )
    {
        await _state.StopConnection();

        return new StandardCommandResult();
    }
}
