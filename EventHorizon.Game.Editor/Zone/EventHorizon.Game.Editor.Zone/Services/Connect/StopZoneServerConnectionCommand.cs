namespace EventHorizon.Game.Editor.Zone.Services.Connect;

using EventHorizon.Game.Client.Core.Command.Model;
using MediatR;

public struct StopZoneServerConnectionCommand : IRequest<StandardCommandResult> { }
