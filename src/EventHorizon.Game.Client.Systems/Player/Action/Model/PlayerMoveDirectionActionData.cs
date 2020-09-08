namespace EventHorizon.Game.Client.Systems.Player.Action.Model
{
    using System;
    using EventHorizon.Game.Client.Systems.Player.Action.Api;
    using EventHorizon.Game.Client.Systems.Player.Modules.Input.Model;

    public struct PlayerMoveDirectionActionData
        : IPlayerActionData
    {
        public MoveDirection MoveDirection { get; }

        public PlayerMoveDirectionActionData(
            MoveDirection moveDirection
        )
        {
            MoveDirection = moveDirection;
        }
    }
}
