namespace EventHorizon.Platform.LogProvider.Send;

using EventHorizon.Game.Client.Core.Command.Model;
using EventHorizon.Platform.LogProvider.Model;
using MediatR;

public struct SendPlatformLogMessageCommand : IRequest<StandardCommandResult>
{
    public PlatformLogMessage Message { get; }

    public SendPlatformLogMessageCommand(PlatformLogMessage message)
    {
        Message = message;
    }
}
