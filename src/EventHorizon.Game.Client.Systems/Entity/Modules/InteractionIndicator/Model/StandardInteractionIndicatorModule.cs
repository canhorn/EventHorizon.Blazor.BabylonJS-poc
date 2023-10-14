namespace EventHorizon.Game.Client.Systems.Entity.Modules.InteractionIndicator.Model;

using System.Threading.Tasks;

using EventHorizon.Game.Client.Engine.Core.Api;
using EventHorizon.Game.Client.Engine.Systems.Entity.Api;
using EventHorizon.Game.Client.Engine.Systems.Module.Model;
using EventHorizon.Game.Client.Systems.Entity.Modules.InteractionIndicator.Api;
using EventHorizon.Game.Client.Systems.Entity.Modules.InteractionIndicator.Clear;
using EventHorizon.Game.Client.Systems.Entity.Modules.InteractionIndicator.Show;
using EventHorizon.Game.Client.Systems.Entity.Properties.Interaction.Api;
using EventHorizon.Game.Client.Systems.Particle.Api;
using EventHorizon.Game.Client.Systems.Particle.Model;

using Microsoft.Extensions.Logging;

public class StandardInteractionIndicatorModule
    : ModuleEntityBase,
        InteractionIndicatorModule,
        ShowInteractionIndicatorEventObserver,
        ClearInteractionIndicatorEventObserver
{
    private readonly ILogger _logger =
        GamePlatform.Logger<StandardInteractionIndicatorModule>();

    private readonly IObjectEntity _entity;
    private ParticleEmitter? _particle;

    public override int Priority => 0;

    public StandardInteractionIndicatorModule(IObjectEntity entity)
    {
        _entity = entity;
    }

    public override async Task Initialize()
    {
        var interactionStateOption =
            _entity.GetPropertyAsOption<InteractionState>(
                InteractionState.NAME
            );
        if (
            !interactionStateOption.HasValue
            || !interactionStateOption.Value.Active
        )
        {
            return;
        }
        var interactionState = interactionStateOption.Value;
        if (interactionState.ParticleTemplate.IsNullOrEmpty())
        {
            _logger.LogPropertyMissing(
                nameof(IObjectEntity),
                nameof(InteractionState),
                nameof(InteractionState.ParticleTemplate)
            );
            return;
        }

        GamePlatform.RegisterObserver(this);

        _particle = new StandardServerParticle(
            _entity,
            new ParticleEmitterOptions(
                GameServiceProvider.GetService<IIndexPool>().NextIndex(),
                interactionState.ParticleTemplate,
                true,
                false
            )
        );
        await _particle.Initialize();
        await _particle.PostInitialize();
    }

    public override async Task Dispose()
    {
        GamePlatform.UnRegisterObserver(this);
        if (_particle.IsNotNull())
        {
            await _particle.Dispose();
        }
    }

    public override Task Update()
    {
        if (_particle.IsNotNull())
        {
            return _particle.Update();
        }
        return Task.CompletedTask;
    }

    public async Task Handle(ShowInteractionIndicatorEvent args)
    {
        if (_particle.IsNull())
        {
            return;
        }

        if (_particle.IsActive)
        {
            await _particle.Stop();
        }

        if (_entity.EntityId != args.EntityId)
        {
            return;
        }

        await _particle.Start();
    }

    public async Task Handle(ClearInteractionIndicatorEvent args)
    {
        if (_particle.IsNotNull() && _particle.IsActive)
        {
            await _particle.Stop();
        }
    }
}
