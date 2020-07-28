namespace EventHorizon.Game.Client.Systems.Connection.Zone.Player.Start
{
    using MediatR;

    public class StartPlayerZoneConnectionCommand
        : IRequest<bool>
    {
        public string ServerUrl { get; }

        public StartPlayerZoneConnectionCommand(
            string serverUrl
        )
        {
            ServerUrl = serverUrl;
        }
    }
}
