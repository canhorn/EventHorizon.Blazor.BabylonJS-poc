namespace EventHorizon.Platform.LogProvider.Process;

using EventHorizon.Game.Client.Core.Command.Model;
using MediatR;

public struct ProcesssPlatformLogMessageCommand : IRequest<StandardCommandResult> { }
