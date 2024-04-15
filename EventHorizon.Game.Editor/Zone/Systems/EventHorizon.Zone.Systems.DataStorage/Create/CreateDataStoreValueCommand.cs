namespace EventHorizon.Zone.Systems.DataStorage.Create;

using EventHorizon.Game.Client.Core.Command.Model;
using MediatR;

public struct CreateDataStoreValueCommand : IRequest<StandardCommandResult>
{
    public string Key { get; }
    public string Type { get; }
    public object Value { get; }

    public CreateDataStoreValueCommand(string key, string type, object value)
    {
        Key = key;
        Type = type;
        Value = value;
    }
}
