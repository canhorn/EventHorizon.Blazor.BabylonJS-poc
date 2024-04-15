namespace EventHorizon.Game.Client.Engine.Particle.State;

using System.Collections.Generic;
using System.Threading.Tasks;
using EventHorizon.Game.Client.Engine.Particle.Api;
using EventHorizon.Game.Client.Engine.Particle.Model;
using EventHorizon.Game.Client.Engine.Systems.Mesh.Model;
using Microsoft.Extensions.Logging;

public class BabylonJSParticleState : ParticleState, ParticleLifecycleService
{
    private readonly ILogger _logger = GamePlatform.Logger<ParticleState>();
    private readonly IDictionary<long, EngineParticleSystem> _particleSystemList =
        new Dictionary<long, EngineParticleSystem>();
    private readonly IDictionary<string, ParticleTemplate> _particleTemplateList =
        new Dictionary<string, ParticleTemplate>();

    public int Priority => 0;

    public BabylonJSParticleState() { }

    public Task Initialize()
    {
        return Task.CompletedTask;
    }

    public async Task Dispose()
    {
        foreach (var particleSystem in _particleSystemList)
        {
            await particleSystem.Value.Dispose();
        }
        _particleSystemList.Clear();
        _particleTemplateList.Clear();
    }

    public Task AddTemplate(ParticleTemplate template)
    {
        _particleTemplateList[template.Id] = template;

        return Task.CompletedTask;
    }

    public Task CreateFromTemplate(long id, string templateId, ParticleSettings settings)
    {
        if (
            !_particleTemplateList.ContainsKey(templateId)
            || templateId.Equals(DefaultParticleSettings.DEFAULT_TEMPLATE_ID)
        )
        {
            var defaultParticleSystem = new BabylonJSEngineParticleSystem(
                id,
                ParticleSettingsModel.Merge(
                    DefaultParticleSettings.Settings,
                    settings,
                    GetGeneratedSettings(settings)
                )
            );
            _particleSystemList[defaultParticleSystem.Id] = defaultParticleSystem;

            _logger.LogPlatformWarning(
                nameof(ParticleState),
                "template_not_found",
                $"TemplateId: {templateId}"
            );

            return Task.CompletedTask;
        }

        if (_particleSystemList.ContainsKey(id))
        {
            _logger.LogPlatformWarning(
                nameof(ParticleState),
                "engine_particle_system_already_exists",
                $"TemplateId: {templateId}"
            );

            return Task.CompletedTask;
        }

        var template = _particleTemplateList[templateId];
        var particleSystem = new BabylonJSEngineParticleSystem(
            id,
            ParticleSettingsModel.Merge(
                template.DefaultSettings,
                settings,
                GetGeneratedSettings(settings)
            )
        );
        _particleSystemList[particleSystem.Id] = particleSystem;

        return Task.CompletedTask;
    }

    private static ParticleSettings GetGeneratedSettings(ParticleSettings settings)
    {
        var result = new ParticleSettingsModel();

        foreach (var setting in settings)
        {
            if (setting.Value is BabylonJSEngineMesh engineMesh)
            {
                result.Add(setting.Key, engineMesh.Mesh);
            }
        }

        return result;
    }

    public async Task DisposeSystem(long id)
    {
        if (_particleSystemList.TryGetValue(id, out var system))
        {
            await system.Dispose();
            _particleSystemList.Remove(id);
        }
    }

    public async Task StartSystem(long id)
    {
        if (_particleSystemList.TryGetValue(id, out var system))
        {
            await system.Start();
        }
    }

    public async Task StopSystem(long id)
    {
        if (_particleSystemList.TryGetValue(id, out var system))
        {
            await system.Stop();
        }
    }

    public async Task UpdateSystem(long id, ParticleSettings settings)
    {
        if (_particleSystemList.TryGetValue(id, out var system))
        {
            await system.UpdateSettings(settings);
        }
    }

    public Task Clear()
    {
        // TODO: Should this clear everything (Systems and Templates)
        _particleTemplateList.Clear();
        return Task.CompletedTask;
    }
}
