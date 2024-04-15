namespace EventHorizon.Game.Editor.Client.DataStorage.Components.Modal;

public struct DataValueModalSubmitArgs
{
    public DataValueModalSubmitType Type { get; }
    public DataValueEditModalModel Model { get; }

    public DataValueModalSubmitArgs(DataValueModalSubmitType type, DataValueEditModalModel model)
    {
        Type = type;
        Model = model;
    }
}
