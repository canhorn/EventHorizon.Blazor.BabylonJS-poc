﻿namespace EventHorizon.Game.Client.Systems.Player.Register
{
    using System;
    using EventHorizon.Game.Client.Core.Command.Model;
    using EventHorizon.Game.Client.Systems.Connection.Zone.Player.Api;
    using MediatR;

    public class RegisterPlayerCommand
        : IRequest<StandardCommandResult>
    {
        public IPlayerZoneDetails Player { get; }

        public RegisterPlayerCommand(
            IPlayerZoneDetails player
        )
        {
            Player = player;
        }
    }
}
