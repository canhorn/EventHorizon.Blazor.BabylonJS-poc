namespace EventHorizon.Game.Client.Engine.Settings.Api
{
    public interface IGameSettings
    {
        string CanvasTagId { get; }

        string GetProperty(
            string key
        );
    }
}
