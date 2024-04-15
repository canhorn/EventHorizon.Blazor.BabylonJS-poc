namespace EventHorizon.Game.Editor.Client.Zone.Reload;

using EventHorizon.Game.Client.Core.Command.Model;
using MediatR;

public struct SetReloadOnZoneStateCommand : IRequest<StandardCommandResult>
{
    public bool Reload { get; }

    public SetReloadOnZoneStateCommand(bool reload)
    {
        Reload = reload;
    }
}
