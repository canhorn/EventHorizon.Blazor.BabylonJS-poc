namespace EventHorizon.Game.Editor.Client.Logging.Send
{
    using EventHorizon.Game.Client.Core.Command.Model;
    using MediatR;

    public struct SendPlatformLogMessageCommand
        : IRequest<StandardCommandResult>
    {
        public PlatformLogMessage Message { get; }

        public SendPlatformLogMessageCommand(
            PlatformLogMessage message
        )
        {
            Message = message;
        }
    }
}
