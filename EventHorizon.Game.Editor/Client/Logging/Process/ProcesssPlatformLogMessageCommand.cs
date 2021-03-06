namespace EventHorizon.Game.Editor.Client.Logging.Process
{
    using EventHorizon.Game.Client.Core.Command.Model;
    using MediatR;

    public struct ProcesssPlatformLogMessageCommand
        : IRequest<StandardCommandResult>
    {
    }
}
