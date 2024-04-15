namespace EventHorizon.Game.Client.Systems.Entity.Properties.Move.Model;

using System;
using EventHorizon.Game.Client.Systems.Entity.Properties.Move.Api;

public class MovementStateModel : IMovementState
{
    public decimal Speed { get; set; }
}
