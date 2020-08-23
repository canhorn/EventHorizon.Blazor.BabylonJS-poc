namespace EventHorizon.Game.Client.Engine.Model.Scripting.Api
{
    public interface IClientScript
        : IServerScript
    {
        string Id { get; }
    }
}
