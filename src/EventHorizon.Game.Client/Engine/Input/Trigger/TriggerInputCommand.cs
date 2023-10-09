namespace EventHorizon.Game.Client.Engine.Input.Trigger;

using EventHorizon.Game.Client.Engine.Input.Model;

using MediatR;

public struct TriggerInputCommand : IRequest
{
    public string Key { get; }
    public InputTriggerType TriggerType { get; }

    public TriggerInputCommand(string key, InputTriggerType triggerType)
    {
        Key = key;
        TriggerType = triggerType;
    }
}
