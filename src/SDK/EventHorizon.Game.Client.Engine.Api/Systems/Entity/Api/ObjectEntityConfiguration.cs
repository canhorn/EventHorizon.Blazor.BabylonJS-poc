namespace EventHorizon.Game.Client.Engine.Systems.Entity.Api
{
    using System;

    public interface ObjectEntityConfiguration
    {
        int Count { get; }
        Option<T> Get<T>(string key);
        T Get<T>(
            string key, 
            Func<T> defaultGenerator
        );
    }
}
