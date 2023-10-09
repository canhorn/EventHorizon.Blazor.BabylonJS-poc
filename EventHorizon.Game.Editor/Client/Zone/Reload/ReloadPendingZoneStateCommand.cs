namespace EventHorizon.Game.Editor.Client.Zone.Reload;

using EventHorizon.Game.Client.Core.Command.Model;

using MediatR;

public struct ReloadPendingZoneStateCommand
    : IRequest<StandardCommandResult> { }
