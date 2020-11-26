namespace EventHorizon.Game.Editor.Client.Authentication.Set
{
    using EventHorizon.Game.Client.Core.Command.Model;
    using MediatR;

    public class SetSessionValueCommand
        : IRequest<StandardCommandResult>
    {
        public string Key { get; }
        public string Value { get; }

        public SetSessionValueCommand(
            string key,
            string value
        )
        {
            Key = key;
            Value = value;
        }
    }
}
