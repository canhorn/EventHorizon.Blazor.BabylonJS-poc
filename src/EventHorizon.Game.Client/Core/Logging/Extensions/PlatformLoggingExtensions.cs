namespace Microsoft.Extensions.Logging;

public static class PlatformLoggingExtensions
{
    /// <summary>
    /// Log a message to the Platform Logger so an Owner can view issues they should be able to fix.
    /// </summary>
    /// <param name="logger"></param>
    /// <param name="warningType">System the property is missing from. [Module]</param>
    /// <param name="objectTypeName">The type of object the property is missing from. [Player, AgentEntity, ClientEntity]</param>
    /// <param name="stateTypeName">The State on the ObjectTypeName that the Property is part of.</param>
    /// <param name="propertyTypeName">The name of property that is missing from the StateTypeName.</param>
    public static void LogPropertyMissing(
        this ILogger logger,
        string objectTypeName,
        string stateTypeName,
        string propertyTypeName,
        string warningType = "module"
    )
    {
        logger.LogWarning(
            "[{PropertyTypeName}] missing from [{ObjectTypeName}.{StateTypeName}]. {WarningType} | {ErrorCode}",
            propertyTypeName,
            objectTypeName,
            stateTypeName,
            warningType,
            "state_property_missing"
        );
    }

    public static void LogPlatformWarning(
        this ILogger logger,
        string where,
        string reason,
        string details,
        string warningType = "system"
    )
    {
        logger.LogWarning(
            "Issue in [{PlatformWarningWhere}] with [{PlatformWarningReason}] because [{PlatformWarningDetails}]. [{WarningType}]",
            where,
            reason,
            details,
            warningType
        );
    }
}
