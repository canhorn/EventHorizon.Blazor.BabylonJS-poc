namespace EventHorizon.Game.Editor.Client.Shared.Toast.Model;

using System;

public struct MessageModel
{
    public string Id { get; }
    public string Heading { get; }
    public string Message { get; }
    public MessageLevel Level { get; }
    public DateTimeOffset Timestamp { get; }

    public MessageModel(string heading, string message, MessageLevel level)
    {
        Id = Guid.NewGuid().ToString();
        Heading = heading;
        Message = message;
        Level = level;
        Timestamp = DateTimeOffset.UtcNow;
    }
}
