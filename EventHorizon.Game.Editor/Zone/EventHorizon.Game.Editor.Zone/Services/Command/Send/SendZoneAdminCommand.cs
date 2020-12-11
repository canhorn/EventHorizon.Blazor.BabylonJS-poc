namespace EventHorizon.Game.Editor.Client.Zone.Services.Command.Send
{
    using EventHorizon.Game.Client.Core.Command.Model;
    using MediatR;

    public class SendZoneAdminCommand
        : IRequest<StandardCommandResult>
    {
        public string Command { get; }
        public object Data { get; }

        public SendZoneAdminCommand(
            string command,
            object data
        )
        {
            Command = command;
            Data = data;
        }
    }
}
