namespace EventHorizon.Game.Client.Systems.Connection.Core.Stop;

using EventHorizon.Game.Client.Core.Command.Model;

using MediatR;

public class StopCoreServerConnectionCommand
    : IRequest<StandardCommandResult> { }
