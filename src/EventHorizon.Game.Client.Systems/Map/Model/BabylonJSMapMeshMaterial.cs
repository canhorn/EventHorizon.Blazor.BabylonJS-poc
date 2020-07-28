using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;
using BabylonJS;
using EventHorizon.Blazor.Interop;
using EventHorizon.Game.Client.Engine.Systems.AssetServer.Model;
using EventHorizon.Game.Client.Systems.Map.Api;

namespace EventHorizon.Game.Client.Systems.Map.Model
{
    // MapMeshMaterial
    [JsonConverter(typeof(CachedEntityConverter))]
    public class BabylonJSMapMeshMaterial
        : StandardMaterial
    {
        // TODO: [Random] : Create Random Abstraction 
        private static Random RANDOM = new Random();

        private readonly IMapMeshMaterialDetails _settings;
        //private PointLight _light;
        //private Texture _groundTexture;
        //private Texture _grassTexture;
        //private Texture _snowTexture;
        //private Texture _sandTexture;
        //private Texture _rockTexture;
        //private Texture _blendTexture;
        //private int _sandLimit;
        //private int _rockLimit;
        //private int _snowLimit;

        public BabylonJSMapMeshMaterial(
            IMapMeshMaterialDetails settings,
            string name,
            Light light,
            Scene scene
        ) : base()
        {
            _settings = settings;
            var assetPath = AssetServer.CreateAssetLocationUrl(
                _settings.AssetPath
            );
            var shaderPath = AssetServer.CreateAssetLocationUrl(
                _settings.Shader
            );
            var entity = EventHorizonBlazorInterop.New(
                new string[] { "MapMeshMaterial" },
                settings, 
                name,
                light,
                assetPath,
                shaderPath,
                scene 
            );
            ___guid = entity.___guid;
        }

        public void setLight(
            Light light
        )
        {
            EventHorizonBlazorInterop.Func<CachedEntity>(
                new object[]
                {
                    new string[] { this.___guid, "setLight" }, light
                }
            );
        }
    }
}
