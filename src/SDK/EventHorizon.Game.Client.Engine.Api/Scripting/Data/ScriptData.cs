﻿namespace EventHorizon.Game.Client.Engine.Scripting.Data
{
    using System.Collections.Generic;

    public struct ScriptData
    {
        private readonly IDictionary<string, object> _data;

        public ScriptData(
            IDictionary<string, object> data
        )
        {
            _data = data;
        }

        public T Get<T>(
            string name
        )
        {
            if (!_data.ContainsKey(
                name
            ))
            {
                return default;
            }
            return _data[name].Cast<T>();
        }

        public void Set(
            string name,
            object value
        )
        {
            _data[name] = value;
        }
    }
}
