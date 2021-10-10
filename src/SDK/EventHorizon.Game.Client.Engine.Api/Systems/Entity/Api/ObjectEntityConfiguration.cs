namespace EventHorizon.Game.Client.Engine.Systems.Entity.Api
{
    public interface ObjectEntityConfiguration
    {
        int Count { get; }
        Option<T> Get<T>(string key);
    }
}
