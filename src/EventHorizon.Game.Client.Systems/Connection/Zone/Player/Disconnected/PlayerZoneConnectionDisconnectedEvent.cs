namespace EventHorizon.Game.Client.Systems.Connection.Zone.Player.Disconnected
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using MediatR;

    public class PlayerZoneConnectionDisconnectedEvent
        : INotification
    {
        public string Code { get; }
        [MaybeNull]
        public Exception Error { get; }

        public PlayerZoneConnectionDisconnectedEvent(
            string code,
            [MaybeNull] Exception error
        )
        {
            Code = code;
            Error = error;
        }
    }
}
