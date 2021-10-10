namespace EventHorizon.Game.Client.Engine.Systems.Entity.Model
{
    using System.Collections.Generic;

    using EventHorizon.Game.Client.Engine.Systems.Entity.Api;

    public class ObjectEntityConfigurationModel
        : Dictionary<string, object>,
        ObjectEntityConfiguration
    {
        public Option<T> Get<T>(
            string key
        )
        {
            if (TryGetValue(
                key,
                out var result
            ))
            {
                return result.To<T>();
            }

            return default(T);
        }
    }
}
