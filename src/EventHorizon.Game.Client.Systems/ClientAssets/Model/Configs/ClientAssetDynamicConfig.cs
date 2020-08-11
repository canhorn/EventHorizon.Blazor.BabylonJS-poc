namespace EventHorizon.Game.Client.Systems.ClientAssets.Model.Configs
{
    using System;
    using System.Buffers;
    using System.Collections.Generic;
    using System.Text.Json;
    using EventHorizon.Game.Client.Systems.ClientAssets.Api;

    public class ClientAssetDynamicConfig
        : IClientAssetConfig
    {
        private readonly IDictionary<string, object> _data;

        public string Type { get; }

        public ClientAssetDynamicConfig(
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
            if (_data.ContainsKey(key)
                && _data[key] is JsonElement typeElement)
            {
                value = typeElement.ToObject<int>();
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
                value = _data[key].Cast<decimal>();
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
                value = _data[key].Cast<float>();
            }
            return value;
        }

        public string GetString(
            string key
        )
        {
            var value = string.Empty;
            if (_data.ContainsKey(key)
                && _data[key] is JsonElement typeElement)
            {
                value = typeElement.ToObject<string>() ?? string.Empty;
            }
            return value;
        }
    }
}
