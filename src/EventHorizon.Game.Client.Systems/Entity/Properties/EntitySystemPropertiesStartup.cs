namespace EventHorizon.Game.Client.Systems.Properties;

using EventHorizon.Game.Client.Core.Mapper.Api;
using EventHorizon.Game.Client.Core.Mapper.Model;
using EventHorizon.Game.Client.Systems.Entity.Properties.Interaction.Api;
using EventHorizon.Game.Client.Systems.Entity.Properties.Interaction.Model;
using EventHorizon.Game.Client.Systems.Entity.Properties.Model.Api;
using EventHorizon.Game.Client.Systems.Entity.Properties.Model.Model;
using EventHorizon.Game.Client.Systems.Entity.Properties.Move.Api;
using EventHorizon.Game.Client.Systems.Entity.Properties.Move.Model;
using EventHorizon.Game.Client.Systems.Entity.Properties.Selection.Api;
using EventHorizon.Game.Client.Systems.Entity.Properties.Selection.Model;

using Microsoft.Extensions.DependencyInjection;

public static class EntitySystemPropertiesStartup
{
    public static IServiceCollection AddEntitySystemPropertiesServices(
        this IServiceCollection services
    ) =>
        services
            .AddSingleton<
                IMapper<InteractionState>,
                StandardMapper<InteractionState, InteractionStateModel>
            >()
            .AddSingleton<
                IMapper<IModelState>,
                StandardMapper<IModelState, ModelStateModel>
            >()
            .AddSingleton<
                IMapper<IMovementState>,
                StandardMapper<IMovementState, MovementStateModel>
            >()
            .AddSingleton<
                IMapper<SelectionState>,
                StandardMapper<SelectionState, SelectionStateModel>
            >();
}
