namespace EventHorizon.Game.Client.Engine.Scripting.Api
{
    public interface IClientScript
        : IServerScript
    {
        string Id { get; }
    }
}
