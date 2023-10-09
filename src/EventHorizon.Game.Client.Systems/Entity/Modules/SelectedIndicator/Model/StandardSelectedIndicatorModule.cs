namespace EventHorizon.Game.Client.Systems.Entity.Modules.SelectedIndicator.Model;

using System.Threading.Tasks;

using EventHorizon.Game.Client.Engine.Core.Api;
using EventHorizon.Game.Client.Engine.Systems.Entity.Api;
using EventHorizon.Game.Client.Engine.Systems.Module.Model;
using EventHorizon.Game.Client.Systems.Entity.Modules.SelectedIndicator.Api;
using EventHorizon.Game.Client.Systems.Entity.Properties.Selection.Api;
using EventHorizon.Game.Client.Systems.Local.ScreenPointer.Entity;
using EventHorizon.Game.Client.Systems.Particle.Api;
using EventHorizon.Game.Client.Systems.Particle.Model;
using EventHorizon.Game.Server.ClientAction.Agent;

using Microsoft.Extensions.Logging;

public class StandardSelectedIndicatorModule
    : ModuleEntityBase,
        SelectedIndicatorModule,
        PointerHitEntityEventObserver,
        ClearPointerHitEntityEventObserver
{
    private readonly ILogger _logger = GameServiceProvider.GetService<
        ILogger<StandardSelectedIndicatorModule>
    >();

    private readonly IObjectEntity _entity;
    private readonly ParticleEmitter _activeParticle;

    public override int Priority => 0;

    public StandardSelectedIndicatorModule(IObjectEntity entity)
    {
        _entity = entity;
        var particleTemplateId = string.Empty;

        var selectedStateOption = _entity.GetPropertyAsOption<SelectionState>(
            SelectionState.NAME
        );
        if (selectedStateOption.HasValue)
        {
            particleTemplateId = selectedStateOption
                .Value
                .SelectedParticleTemplate;
            if (
                selectedStateOption.Value.SelectedParticleTemplate.IsNullOrEmpty()
            )
            {
                _logger.LogPropertyMissing(
                    nameof(IObjectEntity),
                    nameof(SelectionState),
                    nameof(SelectionState.SelectedParticleTemplate)
                );
            }
        }

        _activeParticle = new StandardServerParticle(
            _entity,
            new ParticleEmitterOptions(
                GameServiceProvider.GetService<IIndexPool>().NextIndex(),
                particleTemplateId,
                true,
                false
            )
        );
    }

    public override async Task Initialize()
    {
        GamePlatfrom.RegisterObserver(this);

        await _activeParticle.Initialize();
        await _activeParticle.PostInitialize();
    }

    public override async Task Dispose()
    {
        GamePlatfrom.UnRegisterObserver(this);
        await _activeParticle.Dispose();
    }

    public override Task Update()
    {
        return _activeParticle.Update();
    }

    public async Task Handle(PointerHitEntityEvent args)
    {
        await _activeParticle.Stop();
        if (_entity.EntityId != args.EntityId)
        {
            return;
        }
        await _activeParticle.Start();
    }

    public async Task Handle(ClearPointerHitEntityEvent args)
    {
        await _activeParticle.Stop();
    }
}
