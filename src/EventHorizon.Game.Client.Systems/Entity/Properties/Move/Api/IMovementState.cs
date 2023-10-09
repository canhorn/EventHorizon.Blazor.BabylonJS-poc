namespace EventHorizon.Game.Client.Systems.Entity.Properties.Move.Api;

using System;

public interface IMovementState
{
    public static string NAME = "movementState";

    decimal Speed { get; }
}
