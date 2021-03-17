namespace EventHorizon.Game.Client.Systems.ClientAssets.Config.Model
{
    using System;
    using System.Collections.Generic;
    using EventHorizon.Game.Client.Systems.ClientAssets.Api;

    public class ClientAssetConfigBase
        : ClientAssetConfig
    {
        private readonly IDictionary<string, object> _data;

        public string Type { get; }

        public ClientAssetConfigBase(
            string type,
            IDictionary<string, object> data
        )
        {
            Type = type;
            _data = data;
        }

        public int GetInt(
            string key
        )
        {
            var value = default(int);
            if (_data.ContainsKey(key))
            {
                return _data[key].To<int>();
            }
            return value;
        }

        public decimal GetDecimal(
            string key
        )
        {
            var value = default(decimal);
            if (_data.ContainsKey(key))
            {
                value = _data[key].To<decimal>();
            }
            return value;
        }

        public float GetFloat(
            string key
        )
        {
            var value = default(float);
            if (_data.ContainsKey(key))
            {
                value = _data[key].To<float>();
            }
            return value;
        }

        public string GetString(
            string key
        )
        {
            var value = string.Empty;
            if (_data.ContainsKey(key))
            {
                value = _data[key].To<string>() ?? string.Empty;
            }
            return value;
        }
    }
}
