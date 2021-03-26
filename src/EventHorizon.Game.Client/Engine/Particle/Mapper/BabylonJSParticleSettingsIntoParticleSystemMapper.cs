namespace EventHorizon.Game.Client.Engine.Particle.Mapper
{
    using System;
    using System.Collections.Generic;
    using BabylonJS;
    using EventHorizon.Blazor.Interop;
    using EventHorizon.Game.Client.Engine.Entity.Model;
    using EventHorizon.Game.Client.Engine.Particle.Api;
    using EventHorizon.Game.Client.Engine.Particle.Model;
    using EventHorizon.Game.Client.Engine.Systems.AssetServer.Model;
    using Microsoft.Extensions.Logging;

    public class BabylonJSParticleSettingsIntoParticleSystemMapper
        : ParticleSettingsIntoParticleSystemMapper
    {
        private static IList<string> IGNORE_PROPERTY_LIST => new List<string>
        {
            "name",
            "capacity",
        };
        private static IList<string> VECTOR3_PROPERTY_LIST => new List<string>
        {
            "minEmitBox",
            "maxEmitBox",
            "direction1",
            "direction2",
            "gravity",
        };
        private static IList<string> COLOR4_PROPERTY_LIST => new List<string>
        {
            "color1",
            "color2",
            "colorDead",
        };

        public void Map(
            EngineParticleSystem system,
            ParticleSettings settings
        )
        {
            UpdateFromSettings(
                system.GetBabylonJSParticleSystem(),
                settings
            );
        }

        private static void UpdateFromSettings(
            Option<ParticleSystem> particleSystem,
            ParticleSettings settings
        )
        {
            try
            {
                if (!particleSystem.HasValue)
                {
                    return;
                }
                foreach (var setting in settings)
                {
                    if (setting.Key == "particleTexture")
                    {
                        particleSystem.Value.particleTexture = new Texture(
                            null!,
                            AssetServer.CreateAssetLocationUrl(
                                setting.Value.To(() => string.Empty)
                            )
                        );
                        continue;
                    }
                    // Check for properties to ignore
                    else if (IGNORE_PROPERTY_LIST.Contains(
                        setting.Key
                    ))
                    {
                        continue;
                    }
                    // Check for Vector3 Properties
                    else if (VECTOR3_PROPERTY_LIST.Contains(
                        setting.Key
                    ))
                    {
                        var vector3 = setting.Value.To<Vector3Model>(() => new());
                        SetPropertyOnParticleSystem(
                            particleSystem.Value.___guid,
                            setting.Key,
                            new Vector3(
                                vector3.X,
                                vector3.Y,
                                vector3.Z
                            )
                        );
                        continue;
                    }
                    // Check for Color4 Properties
                    else if (COLOR4_PROPERTY_LIST.Contains(
                        setting.Key
                    ))
                    {
                        var color4 = setting.Value.To(() => new Color4Model());
                        SetPropertyOnParticleSystem(
                            particleSystem.Value.___guid,
                            setting.Key,
                            new Color4(
                                color4.R,
                                color4.G,
                                color4.B,
                                color4.A
                            )
                        );
                        continue;
                    }
                    // Set the Properties
                    SetPropertyOnParticleSystem(
                        particleSystem.Value.___guid,
                        setting.Key,
                        setting.Value
                    );
                }
            }
            catch (Exception ex)
            {
                GameServiceProvider.GetService<ILogger<BabylonJSParticleSettingsIntoParticleSystemMapper>>()
                    .LogError(
                        ex,
                        "Failed to Map Settings into BabylonJS Particle System."
                    );
            }
        }

        private static void SetPropertyOnParticleSystem(
            string guid,
            string property,
            object value
        )
        {
            EventHorizonBlazorInterop.Set(
                guid,
                property,
                value
            );
        }
    }
}
