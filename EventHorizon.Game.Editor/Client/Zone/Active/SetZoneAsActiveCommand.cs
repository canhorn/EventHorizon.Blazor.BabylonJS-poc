namespace EventHorizon.Game.Editor.Client.Zone.Active;

using EventHorizon.Game.Client.Core.Command.Model;
using EventHorizon.Game.Editor.Client.Zone.Api;

using MediatR;

public class SetZoneAsActiveCommand : IRequest<StandardCommandResult>
{
    public ZoneState Zone { get; }

    public SetZoneAsActiveCommand(ZoneState zone)
    {
        Zone = zone;
    }
}
