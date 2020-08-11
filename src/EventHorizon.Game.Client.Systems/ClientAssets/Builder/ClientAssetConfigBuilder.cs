namespace EventHorizon.Game.Client.Systems.ClientAssets.Builder
{
    using System;
    using System.Buffers;
    using System.Collections.Generic;
    using System.Text.Json;
    using EventHorizon.Game.Client.Core.Builder.Api;
    using EventHorizon.Game.Client.Systems.ClientAssets.Api;
    using EventHorizon.Game.Client.Systems.ClientAssets.Model.Configs;
    using Microsoft.Extensions.Logging;

    public class ClientAssetConfigBuilder
        : IBuilder<IClientAssetConfig, IDictionary<string, object>>
    {
        // TODO: Move this into DI, so it can by dynamically updated
        private static readonly IDictionary<string, Func<string, IDictionary<string, object>, IClientAssetConfig>> CONFIG_BUILDERS = new Dictionary<string, Func<string, IDictionary<string, object>, IClientAssetConfig>>
        {
            { "DEFAULT", (type, data) => new ClientAssetDynamicConfig(type, data)},
            { "BOX", (type, data) => new ClientAssetBoxMeshConfig(data) },
            { "GLTF", (type, data) => new ClientAssetGLTFMeshConfig(data) },
            { "JavaScript", (type, data) => new ClientAssetScriptConfig(data) },
            { "SPHERE", (type, data) => new ClientAssetSphereMeshConfig(data) },
        };

        private readonly ILogger _logger;

        public ClientAssetConfigBuilder(
            ILogger<ClientAssetConfigBuilder> logger
        )
        {
            _logger = logger;
        }

        public IClientAssetConfig Build(
            IDictionary<string, object> data
        )
        {
            var type = GetString(data, "type");
            if (CONFIG_BUILDERS.TryGetValue(
                type,
                out var builder
            ))
            {
                return builder(type, data);
            }
            _logger.LogError("Config Type Not found: {ConfigType}", type);
            return CONFIG_BUILDERS["DEFAULT"](
                type,
                data
            );
        }

        public string GetString(
            IDictionary<string, object> data,
            string key
        )
        {
            var value = string.Empty;
            if (data.ContainsKey(key)
                && data[key] is JsonElement typeElement)
            {
                value = typeElement.ToObject<string>() ?? string.Empty;
            }
            return value;
        }
    }
}
