namespace EventHorizon.Game.Client.Engine.Systems.Entity.Api;

using EventHorizon.Game.Client.Engine.Systems.Entity.Model;

using Microsoft.Extensions.Logging;

public static class ObjectEntityExtensions
{
    public static ObjectEntityConfiguration GetEntityConfiguration(
        this IObjectEntity entity
    )
    {
        var config = entity.GetPropertyAsOption<ObjectEntityConfiguration>(
            "entityConfiguration"
        );
        if (config.HasValue.IsNotTrue())
        {
            GamePlatform
                .Logger<IObjectEntity>()
                .LogWarning("Failed to find Entity Configuration.");
            return new ObjectEntityConfigurationModel();
        }

        return config.Value;
    }

    public static ObjectEntityConfiguration GetPlayerConfiguration(
        this IObjectEntity entity
    )
    {
        var config = entity.GetPropertyAsOption<ObjectEntityConfiguration>(
            "playerConfiguration"
        );
        if (config.HasValue.IsNotTrue())
        {
            GamePlatform
                .Logger<IObjectEntity>()
                .LogWarning("Failed to find Player Configuration.");
            return new ObjectEntityConfigurationModel();
        }

        return config.Value;
    }
}
