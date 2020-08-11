namespace EventHorizon.Game.Client.Systems.Connection.Zone.Player.Disconnected
{
    using System;
    using MediatR;

    public class PlayerZoneConnectionDisconnectedEvent
        : INotification
    {
        public string Code { get; }
        public Exception Error { get; }

        public PlayerZoneConnectionDisconnectedEvent(
            string code,
            Exception error
        )
        {
            Code = code;
            Error = error;
        }
    }
}
