namespace EventHorizon.Game.Client.Engine.Systems.Scripting.Model
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using EventHorizon.Game.Client.Engine.Systems.Scripting.Api;

    public class StandardScriptData
        : IScriptData
    {
        private readonly IDictionary<string, object> _data;

        public StandardScriptData(
            IDictionary<string, object> data
        )
        {
            _data = data;
        }

        [return: MaybeNull]
        public T Get<T>(
            string name
        )
        {
            if (_data.TryGetValue(
                name,
                out var value
            ))
            {
                return value.Cast<T>();
            }
            return default;
        }
    }
}
