namespace EventHorizon.Game.Client.Systems.Player.Action.Model;

using System;
using EventHorizon.Game.Client.Engine.Input.Model;
using EventHorizon.Game.Client.Systems.Player.Action.Api;
using EventHorizon.Game.Client.Systems.Player.Modules.Input.Model;

public record PlayerMoveDirectionActionData(MoveDirection MoveDirection)
    : IPlayerActionData;
