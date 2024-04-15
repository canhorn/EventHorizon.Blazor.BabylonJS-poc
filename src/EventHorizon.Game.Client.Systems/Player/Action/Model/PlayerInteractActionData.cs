namespace EventHorizon.Game.Client.Systems.Player.Action.Model;

using System;
using EventHorizon.Game.Client.Systems.Player.Action.Api;

public class PlayerInteractActionData : IPlayerActionData
{
    public long InteractionEntityId { get; }

    public PlayerInteractActionData(long entityId)
    {
        InteractionEntityId = entityId;
    }
}
