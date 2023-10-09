namespace EventHorizon.Game.Client.Engine.Input.Api;

public struct InputKeyEvent
{
    public string Key { get; }

    public InputKeyEvent(string key)
    {
        Key = key;
    }
}
