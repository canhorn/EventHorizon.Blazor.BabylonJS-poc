namespace EventHorizon.Zone.Systems.DataStorage.Update
{
    using EventHorizon.Game.Client.Core.Command.Model;
    using MediatR;

    public struct UpdateDataStoreValueCommand
        : IRequest<StandardCommandResult>
    {
        public string Key { get; }
        public string Type { get; }
        public object Value { get; }

        public UpdateDataStoreValueCommand(
            string key,
            string type,
            object value
        )
        {
            Key = key;
            Type = type;
            Value = value;
        }

    }
}
